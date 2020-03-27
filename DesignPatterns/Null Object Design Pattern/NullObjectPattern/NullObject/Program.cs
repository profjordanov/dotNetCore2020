using System;
using NullObject.Entities;
using NullObject.Services;
using NullObject.View;

namespace NullObject
{
    class Program
    {
        static void Main(string[] args)
        {
            LearnerService learnerService = new LearnerService();
            ILearner learner = learnerService.GetCurrentLearner();
            
            LearnerView view = new LearnerView(learner);
            view.RenderView();
        }
    }
}