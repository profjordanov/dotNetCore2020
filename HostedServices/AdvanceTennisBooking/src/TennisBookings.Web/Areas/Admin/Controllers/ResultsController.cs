using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TennisBookings.ResultsProcessing;
using TennisBookings.Web.BackgroundServices;
using TennisBookings.Web.Configuration;

namespace TennisBookings.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ResultsController : Controller
    {
        private readonly IResultProcessor _resultProcessor;
        private readonly ILogger<ResultsController> _logger;
        private readonly FileProcessingChannel _fileProcessingChannel;
        private readonly IAmazonS3 _amazonS3;
        private readonly string _s3BucketName;

        public ResultsController(
            IResultProcessor resultProcessor,
            ILogger<ResultsController> logger,
            FileProcessingChannel fileProcessingChannel,
            IAmazonS3 amazonS3,
            IOptions<ScoreProcesingConfiguration> options)
        {
            _resultProcessor = resultProcessor;
            _logger = logger;
            _fileProcessingChannel = fileProcessingChannel;
            _amazonS3 = amazonS3;
            _s3BucketName = options.Value.S3BucketName;
        }

        [HttpGet]
        public IActionResult UploadResults()
        {
            return View();
        }

        [HttpGet("v2")]
        public IActionResult UploadResultsV2()
        {
            return View();
        }

        [HttpGet("v3")]
        public IActionResult UploadResultsV3()
        {
            return View();
        }

        [HttpPost("FileUpload")]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {
            var sw = Stopwatch.StartNew();

            if (file is object && file.Length > 0)
            {
                var fileName = Path.GetTempFileName(); // Upload to a temp file path

                await using var stream = new FileStream(fileName, FileMode.Create);

                await file.CopyToAsync(stream);

                stream.Position = 0;

                await _resultProcessor.ProcessAsync(stream);

                System.IO.File.Delete(fileName); // Delete the temp file
            }

            sw.Stop();

            _logger.LogInformation($"Time taken for result upload and processing " +
                $"was {sw.ElapsedMilliseconds}ms.");

            return RedirectToAction("UploadComplete");
        }

        [HttpPost("FileUploadV2")]
        public async Task<IActionResult> FileUploadV2(IFormFile file, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();

            if (file is object && file.Length > 0)
            {
                var fileName = Path.GetTempFileName();

                using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(stream, cancellationToken);
                }

                var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(TimeSpan.FromSeconds(3)); // wait max 3 seconds

                try
                {
                    var fileWritten = await _fileProcessingChannel.AddFileAsync(fileName, cts.Token);

                    if (fileWritten)
                    {
                        sw.Stop();

                        _logger.LogInformation($"Time for result upload was {sw.ElapsedMilliseconds}ms.");

                        return RedirectToAction("UploadComplete");
                    }
                }
                catch (OperationCanceledException) when (cts.IsCancellationRequested)
                {
                    System.IO.File.Delete(fileName); // Delete the temp file to cleanup
                }
            }

            return RedirectToAction("UploadFailed");
        }

        [HttpPost("FileUploadV3")]
        public async Task<IActionResult> FileUploadV3(IFormFile file, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();

            if (file is object && file.Length > 0)
            {
                var objectKey = $"{Guid.NewGuid()}.csv";

                try
                {
                    using var fileTransferUtility = new TransferUtility(_amazonS3);

                    await using var uploadedFileStream = file.OpenReadStream();

                    await fileTransferUtility.UploadAsync(uploadedFileStream, _s3BucketName, objectKey, cancellationToken);
                }
                catch (Exception)
                {
                    return RedirectToAction("UploadFailed");
                }
            }

            sw.Stop();

            _logger.LogInformation($"Time for result upload and processing was {sw.ElapsedMilliseconds}ms.");

            return RedirectToAction("UploadComplete");
        }

        [HttpGet("FileUploadComplete")]
        public IActionResult UploadComplete()
        {
            return View();
        }

        [HttpGet("FileUploadFailed")]
        public IActionResult UploadFailed()
        {
            return View();
        }
    }
}
