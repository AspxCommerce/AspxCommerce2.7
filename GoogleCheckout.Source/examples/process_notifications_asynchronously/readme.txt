This example demonstrates how to process notifications asynchronously. The basic 
idea is that you can process notifications by having a light-weight listener 
that merely records notifications sent by Google. Then another process would 
pick up the notifications and do the heavy processing, like talking to your 
order management system. There are several advantages to this approach:

1. Increased scalability. Because the notification listener is fast, it can 
receive heavy notification volume without failing. The the heavier notification 
processor can catch up on the stored notifications at leisure.

2. Easier testing. You don't have to wait for Google to send you a notification 
to test your notification processor. Instead you can run it over and over again 
against canned notification files. This way it's also easier to test 
interactively with a debugger.

3. Easier troubleshooting. If your notification processor ever fails, you have 
the notification XML on file and re-process it to find out exactly what went 
wrong.


This folder contains a simple file-based queue-processor built on top of the 
.NET Checkout classes. The sub-directories are:

NotificationQueue - This class library is the meat of the queue processor. 
Includes plenty of unit tests.

Listener - Simple web page that uses the NotificationQueue classes to write 
inbound notifications to file.

Processor - Simple executable that uses the NotificationQueue classes to 
process notifications written to file by the Listener.



