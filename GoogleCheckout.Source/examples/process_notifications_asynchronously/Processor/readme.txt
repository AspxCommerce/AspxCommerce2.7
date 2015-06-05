This is a aimple executable that uses the NotificationQueue classes to process 
notifications written to file by the Listener. Note that you need to copy the 
latest GCheckout.dll into this directory to compile the executable.


It requires these keys in the config file to run:

InboxDir - Directory to poll for new notifications to process.

InProcessDir - Directory thst holds notifications while they are being 
processed. If the app terminates unexpectedly, any notification files in this 
directory have probably not been processed.

SuccessDir - Archive directory where notification files go after they have been 
successfully processed.

FailureDir - Directory for notification files that caused an error.


