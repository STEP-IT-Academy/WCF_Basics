# Write a SOA application that implements chat
#### The server part of the application must be a WCF service implemented as a dll hosted in the Windows service.
#### The client must be either a WindowsForms application or a WPF application. Of course, the client must communicate with the service in duplex mode. Starting each client should lead to the creation of a session, within the boundaries of which this client will communicate with the server part. The chat should implement both the "General" chat feature and the ability of clients to communicate in "one-on-one" mode.
#### Each client should display a list of all active participants. The service should notify all active clients when each client joins the chat and exits the chat.
