

//using System.Threading.Tasks;
//using CollabTaskManager.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace CollabTaskManager.Controllers
//{
//    [Route("api/analytics")]
//    [ApiController]
//    public class AnalyticsController : ControllerBase
//    {
//        private readonly IAnalyticsService _analyticsService;

//        public AnalyticsController(IAnalyticsService analyticsService)
//        {
//            _analyticsService = analyticsService;
//        }

//        [HttpGet("task-status")]
//        public async Task<IActionResult> GetTaskStatusAnalytics()
//        {
//            var stats = await _analyticsService.GetTaskStatusCount();
//            return Ok(stats);
//        }

//        [HttpGet("tasks-per-user")]
//        public async Task<IActionResult> GetTasksPerUser()
//        {
//            var stats = await _analyticsService.GetTasksPerUser();
//            return Ok(stats);
//        }

//        [HttpGet("completion-trends")]
//        public async Task<IActionResult> GetCompletionTrends()
//        {
//            var trends = await _analyticsService.GetCompletionTrends();
//            return Ok(trends);
//        }

//        [HttpGet("team-productivity")]
//        public async Task<IActionResult> GetTeamProductivity()
//        {
//            var productivityStats = await _analyticsService.GetTeamProductivityMetrics();
//            return Ok(productivityStats);
//        }
//    }
//}


//using System.Threading.Tasks;
//using CollabTaskManager.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace CollabTaskManager.Controllers
//{
//    [Route("api/analytics")]
//    [ApiController]
//    public class AnalyticsController : ControllerBase
//    {
//        private readonly IAnalyticsService _analyticsService;

//        public AnalyticsController(IAnalyticsService analyticsService)
//        {
//            _analyticsService = analyticsService;
//        }

//        [HttpGet("task-status")]
//        public async Task<IActionResult> GetTaskStatusAnalytics()
//        {
//            var stats = await _analyticsService.GetTaskStatusCount();
//            return Ok(stats);
//        }

//        [HttpGet("tasks-per-user")]
//        public async Task<IActionResult> GetTasksPerUser()
//        {
//            var stats = await _analyticsService.GetTasksPerUser();
//            return Ok(stats);
//        }

//        [HttpGet("completion-trends")]
//        public async Task<IActionResult> GetCompletionTrends()
//        {
//            var trends = await _analyticsService.GetCompletionTrends();
//            return Ok(trends);
//        }

//        [HttpGet("team-productivity")]
//        public async Task<IActionResult> GetTeamProductivity()
//        {
//            var productivityStats = await _analyticsService.GetTeamProductivityMetrics();
//            return Ok(productivityStats);
//        }

//        [HttpGet("report")]
//        public async Task<IActionResult> GetAnalyticsReport()
//        {
//            var report = await _analyticsService.GetAnalyticsReportAsync();
//            return Ok(report);
//        }
//    }
//}

using System.Threading.Tasks;
using CollabTaskManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CollabTaskManager.Controllers
{
    [Route("api/analytics")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        /// <summary>
        /// Retrieves task status count analytics.
        /// </summary>
        /// <returns>A dictionary containing task statuses and their respective counts.</returns>
        /// <response code="200">Returns the task status counts.</response>
        /// <response code="500">If an error occurs while fetching data.</response>
        [HttpGet("task-status")]
        [ProducesResponseType(typeof(Dictionary<string, int>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTaskStatusAnalytics()
        {
            var stats = await _analyticsService.GetTaskStatusCount();
            return Ok(stats);
        }

        /// <summary>
        /// Retrieves the number of tasks assigned to each user.
        /// </summary>
        /// <returns>A dictionary containing users and their task counts.</returns>
        /// <response code="200">Returns the task count per user.</response>
        /// <response code="500">If an error occurs while fetching data.</response>
        [HttpGet("tasks-per-user")]
        [ProducesResponseType(typeof(Dictionary<string, int>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTasksPerUser()
        {
            var stats = await _analyticsService.GetTasksPerUser();
            return Ok(stats);
        }

        /// <summary>
        /// Retrieves task completion trends over time.
        /// </summary>
        /// <returns>A dictionary with completion dates and task completion counts.</returns>
        /// <response code="200">Returns the task completion trends.</response>
        /// <response code="500">If an error occurs while fetching data.</response>
        [HttpGet("completion-trends")]
        [ProducesResponseType(typeof(Dictionary<string, int>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCompletionTrends()
        {
            var trends = await _analyticsService.GetCompletionTrends();
            return Ok(trends);
        }

        /// <summary>
        /// Retrieves team productivity metrics.
        /// </summary>
        /// <returns>A dictionary with users and the number of tasks they completed.</returns>
        /// <response code="200">Returns team productivity metrics.</response>
        /// <response code="500">If an error occurs while fetching data.</response>
        [HttpGet("team-productivity")]
        [ProducesResponseType(typeof(Dictionary<string, int>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTeamProductivity()
        {
            var productivityStats = await _analyticsService.GetTeamProductivityMetrics();
            return Ok(productivityStats);
        }

        /// <summary>
        /// Generates an analytics report.
        /// </summary>
        /// <returns>A string containing the analytics report.</returns>
        /// <response code="200">Returns the analytics report.</response>
        /// <response code="500">If an error occurs while generating the report.</response>
        [HttpGet("report")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAnalyticsReport()
        {
            var report = await _analyticsService.GetAnalyticsReportAsync();
            return Ok(report);
        }
    }
}
