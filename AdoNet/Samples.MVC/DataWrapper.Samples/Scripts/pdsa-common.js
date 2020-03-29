$(document).ready(function () {
  $("[data-pdsa-action]").on("click", function (e) {
    var submit = true;

    e.preventDefault();

    // Check for Delete
    if ($(this).data("pdsa-action") === "delete") {
      if (!confirm("Delete this Product?")) {
        submit = false;
      }
    }

    $("#EventAction").val(
      $(this).data("pdsa-action"));

    $("#EventValue").val(
      $(this).data("pdsa-value"));

    if (submit) {
      $("form").submit();
    }
  });
});