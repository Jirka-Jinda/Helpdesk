using Domain.Messaging;
using System.Threading.Tasks;
using static Tests.DomainTests.DataObjects;

namespace Tests.DomainTests.MessagingTests;

public class ThreadTests
{
    public ThreadTests()
    {
        
    }

    [Fact]
    public async Task Adding_messages_remains_ordered()
    {
        int iterations = 6;

        var ticket = emptyTicket;
        var lateContent = new MessageContent();
        lateContent.Text = "-1";
        await Task.Delay(2);

        for (int i = 0; i < iterations; i++)
        {
            var content = new MessageContent();
            content.Text = i.ToString();
            ticket.Thread.AddMessage(content);
        }

        ticket.Thread.AddMessage(lateContent);


    }
}
