

//using CollabTaskManager.Data;
//using CollabTaskManager.Services.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CollabTaskManager.Services.Implementations
//{
//    public class AnalyticsService : IAnalyticsService
//    {
//        private readonly AppDbContext _context;
//        private readonly ILogger<AnalyticsService> _logger;

//        public AnalyticsService(AppDbContext context, ILogger<AnalyticsService> logger)
//        {
//            _context = context;
//            _logger = logger;
//        }

//        public async Task<Dictionary<string, int>> GetTaskStatusCount()
//        {
//            try
//            {
//                _logger.LogInformation("Fetching task status count...");

//                var tasks = await _context.Tasks.ToListAsync();

//                var taskCounts = tasks
//                    .GroupBy(t => t.Status)
//                    .Select(g => new { Status = g.Key, Count = g.Count() })
//                    .ToDictionary(t => t.Status.ToString(), t => t.Count);

//                _logger.LogInformation("Successfully fetched task status count.");
//                return taskCounts;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error fetching task status count.");
//                throw new Exception("An error occurred while retrieving task status data.");
//            }
//        }

//        public async Task<Dictionary<string, int>> GetTasksPerUser()
//        {
//            try
//            {
//                _logger.LogInformation("Fetching tasks per user...");

//                var tasks = await _context.Tasks.ToListAsync();

//                var tasksPerUser = tasks
//                    .GroupBy(t => t.AssignedTo)
//                    .Select(g => new { User = g.Key, Count = g.Count() })
//                    .ToDictionary(t => t.User, t => t.Count);

//                _logger.LogInformation("Successfully fetched tasks per user.");
//                return tasksPerUser;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error fetching tasks per user.");
//                throw new Exception("An error occurred while retrieving tasks per user.");
//            }
//        }

//        public async Task<string> GetAnalyticsReportAsync()
//        {
//            try
//            {
//                _logger.LogInformation("Generating analytics report...");
//                await Task.Delay(500); // Simulate async processing
//                _logger.LogInformation("Successfully generated analytics report.");
//                return "Analytics Report Data";
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error generating analytics report.");
//                throw new Exception("An error occurred while generating the analytics report.");
//            }
//        }
//    }
//}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using CollabTaskManager.Data;
//using CollabTaskManager.Services.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;

//namespace CollabTaskManager.Services.Implementations
//{
//    public class AnalyticsService : IAnalyticsService
//    {
//        private readonly AppDbContext _context;
//        private readonly ILogger<AnalyticsService> _logger;

//        public AnalyticsService(AppDbContext context, ILogger<AnalyticsService> logger)
//        {
//            _context = context;
//            _logger = logger;
//        }

//        /// <summary>
//        /// Gets task count grouped by status.
//        /// </summary>
//        public async Task<Dictionary<string, int>> GetTaskStatusCount()
//        {
//            try
//            {
//                _logger.LogInformation("Fetching task status count...");

//                var taskCounts = await _context.Tasks
//                    .GroupBy(t => t.Status)
//                    .Select(g => new { Status = g.Key, Count = g.Count() })
//                    .ToDictionaryAsync(t => t.Status.ToString(), t => t.Count);

//                _logger.LogInformation("Successfully fetched task status count.");
//                return taskCounts;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error fetching task status count.");
//                throw;
//            }
//        }

//        /// <summary>
//        /// Gets the number of tasks assigned to each user.
//        /// </summary>
//        public async Task<Dictionary<string, int>> GetTasksPerUser()
//        {
//            try
//            {
//                _logger.LogInformation("Fetching tasks per user...");

//                var tasksPerUser = await _context.Tasks
//                    .Where(t => !string.IsNullOrEmpty(t.AssignedTo))
//                    .GroupBy(t => t.AssignedTo)
//                    .Select(g => new { User = g.Key, Count = g.Count() })
//                    .ToDictionaryAsync(t => t.User, t => t.Count);

//                _logger.LogInformation("Successfully fetched tasks per user.");
//                return tasksPerUser;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error fetching tasks per user.");
//                throw;
//            }
//        }

//        /// <summary>
//        /// Gets task completion trends over the last 30 days.
//        /// </summary>
//        public async Task<Dictionary<string, int>> GetCompletionTrends()
//        {
//            try
//            {
//                _logger.LogInformation("Fetching task completion trends...");

//                var last30Days = DateTime.UtcNow.AddDays(-30);

//                var completionTrends = await _context.Tasks
//                    .Where(t => t.Status == "Completed" && t.UpdatedAt >= last30Days)
//                    .GroupBy(t => t.UpdatedAt.Value.Date)
//                    .Select(g => new { Date = g.Key, Count = g.Count() })
//                    .OrderBy(g => g.Date)
//                    .ToDictionaryAsync(g => g.Date.ToString("yyyy-MM-dd"), g => g.Count);

//                _logger.LogInformation("Successfully fetched task completion trends.");
//                return completionTrends;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error fetching task completion trends.");
//                throw;
//            }
//        }

//        /// <summary>
//        /// Gets team productivity metrics (number of completed tasks per user).
//        /// </summary>
//        public async Task<Dictionary<string, int>> GetTeamProductivityMetrics()
//        {
//            try
//            {
//                _logger.LogInformation("Fetching team productivity metrics...");

//                var productivityMetrics = await _context.Tasks
//                    .Where(t => t.Status == "Completed")
//                    .GroupBy(t => t.AssignedTo)
//                    .Select(g => new { User = g.Key, Count = g.Count() })
//                    .ToDictionaryAsync(g => g.User, g => g.Count);

//                _logger.LogInformation("Successfully fetched team productivity metrics.");
//                return productivityMetrics;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error fetching team productivity metrics.");
//                throw;
//            }
//        }

//        /// <summary>
//        /// Generates an analytics report.
//        /// </summary>
//        public async Task<string> GetAnalyticsReportAsync()
//        {
//            try
//            {
//                _logger.LogInformation("Generating analytics report...");
//                await Task.Delay(500); // Simulating async processing
//                _logger.LogInformation("Successfully generated analytics report.");
//                return "Analytics Report Data";
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error generating analytics report.");
//                throw;
//            }
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollabTaskManager.Data;
using CollabTaskManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CollabTaskManager.Services.Implementations
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AnalyticsService> _logger;

        public AnalyticsService(AppDbContext context, ILogger<AnalyticsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Gets task count grouped by status.
        /// </summary>
        public async Task<Dictionary<string, int>> GetTaskStatusCount()
        {
            try
            {
                _logger.LogInformation("Fetching task status count...");

                var taskCounts = await _context.Tasks
                    .GroupBy(t => t.Status)
                    .Select(g => new { Status = g.Key, Count = g.Count() })
                    .ToDictionaryAsync(t => t.Status.ToString(), t => t.Count);

                _logger.LogInformation("Successfully fetched task status count.");
                return taskCounts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching task status count.");
                throw;
            }
        }

        /// <summary>
        /// Gets the number of tasks assigned to each user.
        /// </summary>
        public async Task<Dictionary<string, int>> GetTasksPerUser()
        {
            try
            {
                _logger.LogInformation("Fetching tasks per user...");

                var tasksPerUser = await _context.Tasks
                    .Where(t => !string.IsNullOrEmpty(t.AssignedTo))
                    .GroupBy(t => t.AssignedTo)
                    .Select(g => new { User = g.Key, Count = g.Count() })
                    .ToDictionaryAsync(t => t.User, t => t.Count);

                _logger.LogInformation("Successfully fetched tasks per user.");
                return tasksPerUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching tasks per user.");
                throw;
            }
        }

        /// <summary>
        /// Gets task completion trends over the last 30 days.
        /// </summary>
        public async Task<Dictionary<string, int>> GetCompletionTrends()
        {
            try
            {
                _logger.LogInformation("Fetching task completion trends...");

                var last30Days = DateTime.UtcNow.AddDays(-30);

                var completionTrends = await _context.Tasks
                    .Where(t => t.Status == "Completed" && t.UpdatedAt >= last30Days)
                    .GroupBy(t => t.UpdatedAt.Date) // ✅ Removed .Value (UpdatedAt is not nullable)
                    .Select(g => new { Date = g.Key, Count = g.Count() })
                    .OrderBy(g => g.Date)
                    .ToDictionaryAsync(g => g.Date.ToString("yyyy-MM-dd"), g => g.Count);

                _logger.LogInformation("Successfully fetched task completion trends.");
                return completionTrends;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching task completion trends.");
                throw;
            }
        }

        /// <summary>
        /// Gets team productivity metrics (number of completed tasks per user).
        /// </summary>
        public async Task<Dictionary<string, int>> GetTeamProductivityMetrics()
        {
            try
            {
                _logger.LogInformation("Fetching team productivity metrics...");

                var productivityMetrics = await _context.Tasks
                    .Where(t => t.Status == "Completed")
                    .GroupBy(t => t.AssignedTo)
                    .Select(g => new { User = g.Key, Count = g.Count() })
                    .ToDictionaryAsync(g => g.User, g => g.Count);

                _logger.LogInformation("Successfully fetched team productivity metrics.");
                return productivityMetrics;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching team productivity metrics.");
                throw;
            }
        }

        /// <summary>
        /// Generates an analytics report.
        /// </summary>
        public async Task<string> GetAnalyticsReportAsync()
        {
            try
            {
                _logger.LogInformation("Generating analytics report...");
                await Task.Delay(500); // Simulating async processing
                _logger.LogInformation("Successfully generated analytics report.");
                return "Analytics Report Data";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating analytics report.");
                throw;
            }
        }
    }
}


