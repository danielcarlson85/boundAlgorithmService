Put a message on service bus:

Add implementation for Eventhub, using Azure Service bus

Now its 2 topics:
User
Test

in Startup.cs it starts the event listener against user and test queue
Under api/v1/auth/login - sends a message to service bus and idp will now get the message and print it to debug window in vs2019

Service bus queues:
https://portal.azure.com/#@boundazureboundtechnologies.onmicrosoft.com/resource/subscriptions/e8021013-5096-4509-a43c-cd6abd577ca1/resourceGroups/BoundEventBus/providers/Microsoft.ServiceBus/namespaces/boundeventbus/queues

To put a message manually on SB:

https://portal.azure.com/#@boundazureboundtechnologies.onmicrosoft.com/resource/subscriptions/e8021013-5096-4509-a43c-cd6abd577ca1/resourceGroups/BoundEventBus/providers/Microsoft.ServiceBus/namespaces/boundeventbus/queues/queue/explorer

IDP will then pick it up and print in debug window

