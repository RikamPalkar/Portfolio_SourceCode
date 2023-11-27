using Microsoft.JSInterop;

namespace Portfolio.Shared
{
    public partial class AutoHidingNavMenu
    {
        private bool collapseNavMenu = true;
        private bool isHovered = false;
        private bool isMobileView = false;

        protected override async Task OnInitializedAsync()
        {
            var width = await JS.InvokeAsync<int>("window.getWindowWidth");
            isMobileView = width < 768;
        }

        private void ToggleSidebar() //when it is mobile view
        {
            collapseNavMenu = false;
            isMobileView = !isMobileView;
        }

        private void ExpandNavMenu()
        {
            if (isMobileView) return;
            isHovered = true;
            if (collapseNavMenu) collapseNavMenu = false;
        }

        private void CollapseNavMenu()
        {
            if (isMobileView) return;

            isHovered = false;
            if (!collapseNavMenu) collapseNavMenu = true;
        }
    }
}