using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PH.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }

    public HttpRequest HttpRequestSetup(Dictionary<String, StringValues> query, string body)
    {
        var reqMock = new Mock<HttpRequest>();

        reqMock.Setup(req => req.Query).Returns(new QueryCollection(query));
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(body);
        writer.Flush();
        stream.Position = 0;
        reqMock.Setup(req => req.Body).Returns(stream);
        return reqMock.Object;
    }
}
