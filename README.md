<p align="center"><img src="https://s33.postimg.cc/g8pyewwm7/COOKEDRABBIT_1.jpg"></p>

## CookedRabbit [![AppVeyor](https://img.shields.io/appveyor/ci/houseofcat/cookedrabbit.svg?logo=appveyor)](https://ci.appveyor.com/project/houseofcat/cookedrabbit)
#### .NET 4.7.2 [![NuGet](https://img.shields.io/nuget/dt/CookedRabbit.Library.svg)](https://www.nuget.org/packages/CookedRabbit.Library/) [![NuGet](https://img.shields.io/nuget/v/CookedRabbit.Library.svg)](https://www.nuget.org/packages/CookedRabbit.Library/)
#### NetCore 2.1 [![NuGet](https://img.shields.io/nuget/dt/CookedRabbit.Core.Library.svg)](https://www.nuget.org/packages/CookedRabbit.Core.Library/) [![NuGet](https://img.shields.io/nuget/v/CookedRabbit.Core.Library.svg)](https://www.nuget.org/packages/CookedRabbit.Core.Library/)


CookedRabbit is a simple RabbitMQ wrapper for dealing with channels and connection headaches. It also shows you the natural evolution to common everyday problems with RabbitMQ implementations and how to avoid them. This solution is more orientated to services.  

RabbitMQ as a Service with lightweight dazzle : RaaS ***L dazzle***

Everything starts in the Demo client. Biggest problem I have observed so far is the storing of IModels (RabbitMQ object that represents channels) in containers/ICollections. Makes code prone to memory leaks, thus I thought a good candidate for abstraction. The examples in the CookedRabbit.Demo demonstrate very rudimentary usages of RabbitMQ. It's not supposed to be rocket science. The library is the simplification, removal, and abstraction of common usage code when wrapping RabbitMQ DotNetClient. It continues to add complexity and simplification at the same time in the RabbitBus & RabbitService.

Inspired by the likes of RawRabbit (https://github.com/pardahlman/RawRabbit), I needed a simpler RabbitMQ solution at times for specific situations. The longterm goal is to be modern, lightweight, and KISS. If you need a more thorough/advanced solution, I highly recommend checking out RawRabbit or EastyNetQ.

For more details visit the [Wiki](https://github.com/thyams/CookedRabbit/wiki)!

### Why use CookedRabbit?

<details><summary>Click to show!</summary>
<p>

I personally would use CookedRabbit because it is simple. Once you are setup you just have to Publish and/or Get. Obviously its capable of much more but suffice to say - it is simple. Another benefit, at least from my perspective, is that I will stay current with .Net Framework, NetCore, C#7.x+, and the RabbitMQ client. It is not my intention to let things lag behind Pivotal RabbitMQ or Microsoft releases. It currently supports NET472, NetCore 2.1, and C# 7.3. It isn't just about Security, Microsoft EoL, or performance, it's about sticking with the times. Patterns change and so will CookedRabbit.

Which leads me to the built-in RabbitMQ DotNetClient Options!

```
RabbitMQ Dotnet Client 5.1.0 (6/23/2018)
Changes from Official Release

   All NuGets updated.
   NetFramework 4.5/4.5.1 -> 4.7.2
   NetStandard 1.5 -> 2.0
   C# 7.3 (latest version)
   ApiGen re-compiled.
   Client compiled as x64.

RabbitMQ.Core Project Based off of the official DotNetClient 5.1.0 (6/23/2018)
Changes from Official Release

   NetCore 2.1
   Compiled as C# 7.3 (latest version) and API re-generated.
   Code converted to C# 6+ based on IDE Message / Format recommendations.
   License consolidated to a single file.
```

CookedRabbit.Core.Library integrates directly with RabbitMQ.Core for debugging ease.  
CookedRabbit is in active development.  
CookedRabbit supports SSL/TLS.  
CookedRabbit supports Gzip, Deflate, and LZ4 compression.  
CookedRabbit supports Utf8Json, ZeroFormat, and JSON string serialization.  
CookedRabbit provides async/await around RabbitMQ calls.  
CookedRabbit is Dependency Injection friendly.  
CookedRabbit services support an optional ILogger from Microsoft.Extensions.Logger.  
CookedRabbit supports logic based customizations.  
CookedRabbit has fairly decent commenting and Wiki being filled out.  
CookedRabbit has a plethora of examples on how to use in the Demo project, Tests project, and Benchmark project.  
CookedRabbit has a benchmark project.  

</p>
</details>

### Getting Started

<details><summary>Click to show!</summary>
<p>

#### Configuring RabbitMQ Server First (if running Local)
To run .Demo locally, have Erlang 20.3 and Server RabbitMQ v3.7.x installed locally and running first.
Use the HTTP API management from RabbitMQ to verify communication is occurring.
The Demo project calls WarmupAsync() will create the queue '001' to work with, if it doesn't exist, and send/receive a test messages.

Developed/Tested On

  * Erlang 20.3
  * RabbitMQ Server v3.7.5 (pre-7/6/2018)
  * RabbitMQ Server v3.7.7 (post-7/6/2018)

#### Configuration Values
Checkout the RabbitSeasoning to configure your RabbitService/RabbitTopologyService.

#### NetFramework Requirements

<details><summary>Click to show!</summary>
<p>

 * Visual Studio 2017+ installed (Community+).
 * .NET 4.7.2 SDK installed.
 * Compile as C# 7.2+ minimum.

</p>
</details>

#### NetCore Requirements

<details><summary>Click to show!</summary>
<p>

 * Visual Studio Code or Visual Studio 2017+ installed.
 * Open Folder `NetCore` or open the SLN.
 * Compile as C# 7.2+ minimum.
 * NetCore 2.1.0 SDK installed.

*Note: (NetCore runtime 2.1.1 seems buggy at this time)*

</p>
</details>

#### Documentation

<details><summary>Click to show!</summary>
<p>

 * Visit the Wiki!
    * Check out the changelog for detailed changes made via commit.
    * Check out the Library Documentation for class/variable/service descriptions and comments.
    * Check out the Library Tutorial section for basic how-tos.

</p>
</details>

</p>
</details>

### Upcoming Features

<details><summary>Click to show!</summary>
<p>

 * ~~Add XML comments.~~ Done.
 * ~~Create Wiki.~~ Basic one done, generated from XML comments.
 * ~~NuGet packages.~~ Done, Libraries uploaded.
 * ~~Add interfaces to the Pools.~~ Done, everything updated.
 * ~~More robust error handling.~~ Done, will continue pattern.
 * ~~Quality life tools such as compression etc.~~ Done, will add more.
 * Custom Connection model with EventListener wireups.
 * Disaster recovery & circuit break.
 * A ServiceBus-esque client.
 * Additional Demonstrations.
 * ~~Add Xunit test project.~~ Done, will add tests as I go!
 * ~~Add autoscaling for ChannelPools.~~ Done, will tweak it as tests go on.
 * ~~Add TLS/SSL support.~~ Done, will tweak it as issues arise.

</p>
</details>

### Service Topology At A Glance

<details><summary>Click to show ASCII Art!</summary>
<p>

```
    ║
    ║ Your Business Logic
    ║
    ╠══ » RabbbitBurrow ════════════════════════════════════════════════════════════╗
    ║       ║                                                                       ║
    ║       ║ & RabbitSerializeService : RabbitDeliveryService                      ║
    ║       ║ & RabbitMaintenanceService : RabbitTopologyService                    ║
    ║       ║ - Circuit Breaker                                                     ║
    ║       ║ - Abstraction                                                         ║
    ║       ║                                                                       ║
    ╠════ » ╠══ » RabbitDeliveryService : IRabbitDeliveryService ═══════════════════╣
    ║       ║       ║                                                               ║
    ║       ║       ║ & RabbitChannelPool                                           ║
    ║       ║       ║ & RabbitSeasoning                                             ║
    ║       ║       ║                                                               ║
    ║       ║       ║ + Flag Channel As Dead                                        ║
    ║       ║       ║ + Return Channel To Pool (Finished Work)                      ║
    ║       ║       ║                                                               ║
    ║       ║       ║ + Publish                                                     ║
    ║       ║       ║ + PublishMany                                                 ║
    ║       ║       ║ + PublishManyAsBatches                                        ║
    ║       ║       ║                                                               ║
    ║       ║       ║ + Get                                                         ║
    ║       ║       ║   + Returns As ValueTuple                                     ║
    ║       ║       ║   + Returns As AckableResult                                  ║
    ║       ║       ║ + GetMany                                                     ║
    ║       ║       ║   + Returns As ValueTuple                                     ║
    ║       ║       ║   + Returns As AckableResult                                  ║
    ║       ║       ║                                                               ║
    ║       ║       ║ + CreateConsumerAsync                                         ║
    ║       ║       ║ + CreateAsyncConsumerAsync                                    ║
    ║       ║       ║                                                               ║
    ║       ║       ║ Customize:                                                    ║
    ║       ║       ║ + Use ILogger                                                 ║
    ║       ║       ║ + throw ex                                                    ║
    ║       ║       ║ + Throttling                                                  ║
    ║       ║       ║                                                               ║
    ║       ║       ╚══ » RabbitChannelPool : IRabbitChannelPool ═══════════════════╣
    ║       ║               ║                                                       ║
    ║       ║               ║ & RabbitConnectionPool                                ║
    ║       ║               ║ & RabbitSeasoning                                     ║
    ║       ║               ║                                                       ║
    ║       ║               ║ + GetTransientChannel (non-Ackable)                   ║
    ║       ║               ║ + GetTransientChannel (Ackable)                       ║
    ║       ║               ║                                                       ║
    ║       ║               ║ + GetChannelPair from &ChannelPool (non-Ackable)      ║
    ║       ║               ║ + GetChannelPair from &ChannelPool (ackable)          ║
    ║       ║               ║                                                       ║
    ║       ║               ║ Mechanisms:                                           ║
    ║       ║               ║ + Get Channel Delay (When All Channels Are In Use)    ║
    ║       ║               ║ + In Use ChannelPair Pool                             ║
    ║       ║               ║ + In Use Ack ChannelPair Pool                         ║
    ║       ║               ║ + Return Channel to A Pool                            ║
    ║       ║               ║                                                       ║
    ║       ║               ║ Customize:                                            ║
    ║       ║               ║ - Use ILogger                                         ║
    ║       ║               ║ - throw ex                                            ║
    ║       ║               ║                                                       ║
    ║       ║               ╚══ » RabbitConnectionPool : IRabbitConnectionPool ═════╣
    ║       ║                       ║                                               ║
    ║       ║                       ║ & RabbitMQ ConnectionFactory                  ║
    ║       ║                       ║ & RabbitSeasoning                             ║
    ║       ║                       ║ & ConnectionPool                              ║
    ║       ║                       ║                                               ║
    ║       ║                       ║ Customize:                                    ║
    ║       ║                       ║ - Use ILogger                                 ║
    ║       ║                       ║ - throw ex                                    ║
    ║       ║                       ║ - System for Dealing with Flagged Connections ║
    ║       ║                       ║                                               ║
    ║       ║                       ╚═══════════════════════════════════════════════╣
    ║       ║                                                                       ║
    ╚═══════╚═══════════════════════════════════════════════════════════════════════╝

To be continued on the wiki!
```

Legend  

    & Indicates mandatory/crucial internal object.  
    + Exists (or exists with future enhancements)  
    - Does not exist yet.  
    ! Important  

</p>
</details>

# [HouseofCat.io](https://houseofcat.io)
<p align="center"><img src="https://s33.postimg.cc/tt2hpn1of/COOKEDRABBIT_Readme_2.jpg"></p>
