using Prometheus;

namespace Glasswall.FileTrust.RepoName.Business.Instrumentation
{
    public static class ApiInstrumentation
    {
        public static readonly Gauge DummyMetric = Metrics
            .CreateGauge("DummyMetric", "DummyMetricHelp", "DummyMetricLabel");
    }
}
