using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moodle.BusinessLogic.Interface;
using Moq;

namespace Moodle.WebApi.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IDictionary<string, object> arguments = new Dictionary<string, object>();
            arguments.Add("userId",1);

            IHeaderDictionary headers = new HeaderDictionary();
            headers.Add("Authorization", Guid.NewGuid().ToString());

            Mock<HttpRequest> mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(r => r.Headers).Returns(headers);

            Mock<ISessionLogic> mockSessionLogic = new Mock<ISessionLogic>();
            mockSessionLogic.Setup(s => s.IsValidToken(It.IsAny<Guid>())).Returns(true);

            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(h => h.Request).Returns(mockHttpRequest.Object);
            mockHttpContext.Setup(h => h.RequestServices.GetService(It.IsAny<Type>())).Returns(mockSessionLogic.Object);


            ActionContext actionContext = new ActionContext(
                mockHttpContext.Object, 
                new Mock<RouteData>().Object,
                new Mock<ActionDescriptor>().Object);


            ActionExecutingContext actionExecutingContext = new ActionExecutingContext(
                actionContext, 
                new Mock<IList<IFilterMetadata>>().Object,arguments, 
                new Mock<object>().Object);
            
            AuthenticationFilter filter = new AuthenticationFilter();
            filter.OnActionExecuting(actionExecutingContext);

            Assert.IsTrue(true);
        }
    }
}
