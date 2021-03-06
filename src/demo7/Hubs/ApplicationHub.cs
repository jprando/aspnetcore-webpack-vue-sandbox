using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using demo7.Models;
using System.Threading.Channels;
using System;

namespace demo7.Hubs
{
    public class ApplicationHub : Hub
    {
         public Task Send(ChatMessage message){
            return Clients.All.SendAsync("Send", message.Message);
        }

        public ChannelReader<int> CountDown(int count) {
            var channel = Channel.CreateUnbounded<int>();

            _ = WriteToChannel(channel.Writer, count);

            return channel.Reader;

            async Task WriteToChannel(ChannelWriter<int> writer, int thing) {
                for (int i = thing; i >= 0 ; i--)
                {
                    await writer.WriteAsync(i);
                    await Task.Delay(TimeSpan.FromSeconds(0.75));
                }

                writer.Complete();
            }
        }
    }
}