using System;
using CrochetByJk.Components.Hangifre;
using Hangfire;
using NLog;

namespace CrochetByJk
{
    public static class HangfireConfig
    {
        private static readonly ILogger Logger;
        static HangfireConfig()
        {
            Logger = LogManager.GetLogger("crochetDbLogger");
        }
        public static void Configure()
        {
            try
            {
                RecurringJob.AddOrUpdate("Nazwa zadania", () => HangfireJobs.DeleteGallery(), Cron.Minutely);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Hangfire exception");
            }
        }
    }
}