
using CursosApp.Dtos.Common;
using CursosApp.Dtos.Statistics;

namespace CursosApp.Services.Statistics
{
    public interface IStatisticsService
    {
        Task<ResponseDto<StatisticsDto>> GetCountsAsync();
    }
}