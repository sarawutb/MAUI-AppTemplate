using MAUIPos.Application.Views.Widget;
using System;
using System.Threading;

namespace MAUIPos.Application.Services
{
    public class LoadingService
    {
        private bool _isLoading;
        private LoadingWidget _loading;
        public LoadingService()
        {
        }

        public async Task ShowLoading(Func<CancellationToken, Task> task, bool isCancel = false, bool isDismissed = false)
        {
            if (!_isLoading)
            {
                try
                {
                    var cts = new CancellationTokenSource();
                    _isLoading = true;
                    _loading = new LoadingWidget();
                    await _loading.ShowLoading(task, isCancel, isDismissed, cts);
                    await _loading.HideLoading();
                    _isLoading = false;
                }
                catch (TaskCanceledException)
                {
                    await _loading.HideLoading();
                    _isLoading = false;
                }
            }
        }
    }
}
