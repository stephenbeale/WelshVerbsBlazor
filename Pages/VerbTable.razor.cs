using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using WelshVerbsBlazor.Models;

namespace WelshVerbsBlazor.Pages
{
    public partial class VerbTable : ComponentBase
    {
        #region MyRegion
        [Inject]
        public HttpClient httpClient { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; } = default!;
        
        #endregion

        #region Model
        public EventCallback<bool> IsBusy { get; set; }
        private bool saving = false;
        private string LoadingErrorMessage { get; set; } = string.Empty;
        private bool ShowPageError;

        private List<Verb>? verbList { get; set; } = new();
        private List<string>? myListy { get; set; } = new();

        [Parameter]
        public string? Verb { get; set; }
        #endregion

        #region
        private HttpClient httpClientVerbApi { get; set; } = default!;

        #endregion

        private async Task GetVerb()
        {
            try
            {
                var response = await httpClient.GetAsync("api/verbs/");

                if (response.IsSuccessStatusCode)
                {
                    await response.Content.ReadAsStringAsync();
                }
            }
            catch(Exception ex)
            {
                LoadingErrorMessage = $"An error occurred: {ex.Message}";
                ShowPageError = true;
                return ;
            }
        }

        public async Task<string> GetRegister()
        {
            try
            {
                // Assuming your API endpoint is at the same base address as your Blazor app
                var response = await httpClient.GetAsync("api/verbs/register");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    LoadingErrorMessage = $"Error: {response.StatusCode}";
                    ShowPageError = true;
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                LoadingErrorMessage = $"An error occurred: {ex.Message}";
                ShowPageError = true;
                return string.Empty;
            }
        }
    }
}
