using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace FiapCadContato.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestTelemetryController : ControllerBase
    {
        Counter _counter;
        Histogram _histogram;
        Gauge _gauge;
        Summary _summary;

        public TestTelemetryController()
        {
            _counter = Metrics.CreateCounter("TestMetricCounter", "will be incremented and published as metrics");
            _histogram = Metrics.CreateHistogram("TestMetricHistogram", "Will observe a value and publish it as Histogram");
            _gauge = Metrics.CreateGauge("TestMetricGauge", "Will observe a value and publish it as Gauge");
            _summary = Metrics.CreateSummary("TestMetricSummary", "Will observe a value and publish it as Summary");
        }


        [HttpPost("IncrementCounter")]
        public void PostCounter(int inc)
        {
            _counter.Inc(inc);
            _counter.Publish();
        }

        [HttpPost("IncrementHistogram")]
        public void PostHistogram(int inc)
        {
            _histogram.Observe(inc);
            _histogram.Publish();
        }

        [HttpPost("IncrementGauge")]
        public void IncrementGauge(int inc)
        {
            _gauge.Inc(inc);
            _gauge.Publish();
        }

        [HttpPost("DecrementGauge")]
        public void DecrementGauge(int inc)
        {
            _gauge.Dec(inc);
            _gauge.Publish();
        }

        [HttpPost("IncrementSummary")]
        public void PostSummary(int inc)
        {
            _summary.Observe(inc);
            _summary.Publish();
        }
    }
}
