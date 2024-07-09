using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirginMedia.Sales.Web.Models;

namespace VirginMedia.Sales.Web.Components
{
    public partial class Pagination
    {
        [Parameter]
        public PagingData PagingData { get; set; }
        [Parameter]
        public int Spread { get; set; }
        [Parameter]
        public EventCallback<int> SelectedPage { get; set; }
        
        private List<PagingLink> _links;

        protected override void OnParametersSet() 
        {
            CreatePaginationLinks();
        }

        private void CreatePaginationLinks()
        {
            _links = new List<PagingLink>();

            _links.Add(new PagingLink(PagingData.CurrentPage - 1, PagingData.HasPrevious, "Previous"));

            for (int i = 1; i <= PagingData.TotalPages; i++)
            {
                if(i >= PagingData.CurrentPage - Spread && i <= PagingData.CurrentPage + Spread)
                {
                    _links.Add(new PagingLink(i, true, i.ToString()) { Active = PagingData.CurrentPage == i });
                }
            }

            _links.Add(new PagingLink(PagingData.CurrentPage + 1, PagingData.HasNext, "Next"));
        }

        private async Task OnSelectedPage(PagingLink link)
        {
            if (link.Page == PagingData.CurrentPage || !link.Enabled)
                return;

            PagingData.CurrentPage = link.Page;
            await SelectedPage.InvokeAsync(link.Page);
        }
    }
}
