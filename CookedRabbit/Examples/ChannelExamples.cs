﻿using CookedRabbit.Pools;
using CookedRabbit.Services;
using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static CookedRabbit.DemoHelper;

namespace CookedRabbit
{
    public static class ChannelExamples
    {
        // Using a Connection Pool
        public static async Task RunManualTransientChannelTestAsync()
        {
            var rcp = await RabbitConnectionPool.CreateRabbitConnectionPoolAsync("localhost", Environment.CurrentDirectory);

            var sendMessage = SendMessagesForeverAsync(rcp);
            var receiveMessage = ReceiveMessagesForeverAsync(rcp);

            await Task.WhenAll(new Task[] { sendMessage, receiveMessage });
        }

        public static async Task SendMessagesForeverAsync(RabbitConnectionPool rcp)
        {
            ResetThreadName(Thread.CurrentThread, "Transient SendMessagesForever Thread");
            int counter = 0;
            while (true)
            {
                var chan1 = rcp.GetConnection().CreateModel();
                var chan2 = rcp.GetConnection().CreateModel();
                var chan3 = rcp.GetConnection().CreateModel();

                var task1 = SendMessageAsync(chan1, counter++);
                var task2 = SendMessageAsync(chan2, counter++);
                var task3 = SendMessageAsync(chan3, counter++);

                await Task.WhenAll(new Task[] { task1, task2, task3 });

                chan1.Close(200, happyShutdown);
                chan1.Dispose();

                chan2.Close(200, happyShutdown);
                chan2.Dispose();

                chan3.Close(200, happyShutdown);
                chan3.Dispose();
            }
        }

        public static async Task ReceiveMessagesForeverAsync(RabbitConnectionPool rcp)
        {
            ResetThreadName(Thread.CurrentThread, "Transient ReceiveMessagesForever Thread");
            while (true)
            {
                var chan1 = rcp.GetConnection().CreateModel();
                var chan2 = rcp.GetConnection().CreateModel();
                var chan3 = rcp.GetConnection().CreateModel();

                if (chan1.MessageCount(queueName) > 0)
                {
                    var task1 = ReceiveMessageAsync(chan1);
                    var task2 = ReceiveMessageAsync(chan2);
                    var task3 = ReceiveMessageAsync(chan3);

                    await Task.WhenAll(new Task[] { task1, task2, task3 });
                }

                chan1.Close(200, happyShutdown);
                chan1.Dispose();

                chan2.Close(200, happyShutdown);
                chan2.Dispose();

                chan3.Close(200, happyShutdown);
                chan3.Dispose();
            }
        }

        // Using a Connection Pool
        public static async Task RunPoolChannelTestAsync()
        {
            var rcp = await RabbitChannelPool.CreateRabbitChannelPoolAsync("localhost", "localhost");

            var sendMessage = PoolChannel_SendMessagesForeverAsync(rcp);
            var receiveMessage = PoolChannel_ReceiveMessagesForeverAsync(rcp);

            await Task.WhenAll(new Task[] { sendMessage, receiveMessage });
        }

        public static async Task PoolChannel_SendMessagesForeverAsync(RabbitChannelPool rcp)
        {
            ResetThreadName(Thread.CurrentThread, "PoolChannel SendMessagesForever Thread");
            int counter = 0;
            while (true)
            {
                var task1 = SendMessageAsync((await rcp.GetPooledChannelPair()).Channel, counter++);
                var task2 = SendMessageAsync((await rcp.GetPooledChannelPair()).Channel, counter++);
                var task3 = SendMessageAsync((await rcp.GetPooledChannelPair()).Channel, counter++);

                await Task.WhenAll(new Task[] { task1, task2, task3 });
            }
        }

        public static async Task PoolChannel_ReceiveMessagesForeverAsync(RabbitChannelPool rcp)
        {
            ResetThreadName(Thread.CurrentThread, "PoolChannel ReceiveMessagesForever Thread");
            while (true)
            {
                var (ChannelId, Channel) = await rcp.GetPooledChannelPair();
                if (Channel.MessageCount(queueName) > 0)
                {
                    var task1 = ReceiveMessageAsync(Channel);
                    var task2 = ReceiveMessageAsync((await rcp.GetPooledChannelPair()).Channel);
                    var task3 = ReceiveMessageAsync((await rcp.GetPooledChannelPair()).Channel);
                    var task4 = ReceiveMessageAsync((await rcp.GetPooledChannelPair()).Channel);
                    var task5 = ReceiveMessageAsync((await rcp.GetPooledChannelPair()).Channel);
                    var task6 = ReceiveMessageAsync((await rcp.GetPooledChannelPair()).Channel);

                    await Task.WhenAll(new Task[] { task1, task2, task3, task4, task5, task6 });
                }
            }
        }

        // Using RabbitService
        public static RabbitService _rabbitService;
        public static Random rand = new Random();

        public static async Task RunRabbitServicePoolChannelTestAsync()
        {
            _rabbitService = new RabbitService("localhost", Environment.MachineName);

            await Task.WhenAll(new Task[] { RabbitService_SendMessagesForeverAsync(), RabbitService_ReceiveMessagesForeverAsync() });
        }

        public static async Task RabbitService_SendMessagesForeverAsync()
        {
            ResetThreadName(Thread.CurrentThread, "RabbitService_Send");

            var count = 0;
            while (true)
            {
                await Task.Delay(rand.Next(0, 2));

                var task1 = _rabbitService.PublishAsync(queueName, await GetRandomByteArray(4000));
                var task2 = _rabbitService.PublishAsync(queueName, await GetRandomByteArray(4000));

                await Task.WhenAll(new Task[] { task1, task2 });

                count++;
            }
        }

        public static async Task RabbitService_ReceiveMessagesForeverAsync()
        {
            ResetThreadName(Thread.CurrentThread, "RabbitService_Receive");
            while (true)
            {
                await Task.Delay(rand.Next(0, 2));

                var task1 = _rabbitService.GetManyAsync(queueName, 100);

                await Task.WhenAll(new Task[] { task1 });
            }
        }

        // Using RabbitService And Checking For Accuracy
        public static async Task RunRabbitServiceAccuracyTestAsync()
        {
            _rabbitService = new RabbitService("localhost", Environment.MachineName);

            await Task.WhenAll(new Task[] { RabbitService_ReceiveMessagesForeverWithAccuracyAsync(), RabbitService_SendMessagesForeverWithAccuracyAsync() });
        }

        private static ConcurrentDictionary<string, bool> _accuracyCheck = new ConcurrentDictionary<string, bool>();

        public static async Task RabbitService_SendMessagesForeverWithAccuracyAsync()
        {
            var count = 0;
            while (true)
            {
                //await Task.Delay(rand.Next(0, 2));

                var message = $"{helloWorld} {count}";
                _accuracyCheck.TryAdd(message, false);

                var task1 = _rabbitService.PublishAsync(queueName, Encoding.UTF8.GetBytes(message));

                await Task.WhenAll(new Task[] { task1 });

                count++;
            }
        }

        public static async Task RabbitService_ReceiveMessagesForeverWithAccuracyAsync()
        {
            while (true)
            {
                //await Task.Delay(rand.Next(0, 2));

                var task1 = _rabbitService.GetManyAsync(queueName, 100);

                await Task.WhenAll(new Task[] { task1 });

                foreach(var result in task1.Result)
                {
                    var message = Encoding.UTF8.GetString(result.Body);
                    if (_accuracyCheck.ContainsKey(message))
                    {
                        _accuracyCheck[message] = true;
                    }
                }
            }
        }
    }
}