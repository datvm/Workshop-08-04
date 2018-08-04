using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Api.Models.Services;
using Xunit;

namespace Workshop.Test.Api.Models.Services
{

    public class GoogleServiceTest
    {

        private IGoogleService googleService;

        public GoogleServiceTest()
        {
            this.googleService = new GoogleService(new Workshop.Api.Models.ApiSettings()
            {

            });
        }

        [Fact]
        public void GetOAuth2Url()
        {
            var state = Guid.NewGuid().ToString();
            var result = this.googleService.GetOAuth2Url(state);

            Assert.NotNull(result);
        }

    }

}
