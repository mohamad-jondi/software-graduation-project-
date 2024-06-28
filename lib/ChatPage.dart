import 'package:flutter/material.dart';
import 'package:flutter_app/providers/AppProvider.dart';
import 'package:provider/provider.dart';

class ChatPage extends StatelessWidget {
  final String contactName;
  final String contactImage;

  ChatPage({required this.contactName, required this.contactImage});

  @override
  Widget build(BuildContext context) {
    return Consumer<AppProvider>(builder: (context, provider, x) {
      var l = provider.chats
          .where((e) =>
              e.firstPartyUserName == contactName ||
              e.secondPartyUsername == contactName)
          .toList()
          .length;
      return Scaffold(
        appBar: AppBar(
          title: Text(contactName),
          backgroundColor: Color(0xFF199A8E),
          iconTheme: IconThemeData(color: Colors.white),
          titleTextStyle: TextStyle(color: Colors.white, fontSize: 20),
        ),
        body: Column(
          children: [
            Expanded(
              child: ListView(
                padding: EdgeInsets.all(16.0),
                children: [
                  // Add chat messages here
                  // Example: Chat bubble
                  ...(l > 0
                          ? provider.chats
                                  .firstWhere((e) =>
                                      e.firstPartyUserName == contactName ||
                                      e.secondPartyUsername == contactName)
                                  .messages ??
                              []
                          : [])
                      .map(
                        (e) => e.senderUsername != provider.loggedUser.username
                            ? Align(
                                alignment: Alignment.centerLeft,
                                child: Container(
                                  margin: EdgeInsets.symmetric(vertical: 4.0),
                                  padding: EdgeInsets.all(12.0),
                                  decoration: BoxDecoration(
                                    color: Colors.grey[300],
                                    borderRadius: BorderRadius.circular(8.0),
                                  ),
                                  child: Text(e.messageContent),
                                ),
                              )
                            : Align(
                                alignment: Alignment.centerRight,
                                child: Container(
                                  margin: EdgeInsets.symmetric(vertical: 4.0),
                                  padding: EdgeInsets.all(12.0),
                                  decoration: BoxDecoration(
                                    color: Color(0xFF199A8E),
                                    borderRadius: BorderRadius.circular(8.0),
                                  ),
                                  child: Text(
                                    e.messageContent,
                                    style: TextStyle(color: Colors.white),
                                  ),
                                ),
                              ),
                      )
                      .toList()
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: Row(
                children: [
                  Expanded(
                    child: TextField(
                      controller: provider.messageController,
                      decoration: InputDecoration(
                        hintText: 'Type a message',
                        border: OutlineInputBorder(
                          borderRadius: BorderRadius.circular(8.0),
                        ),
                      ),
                    ),
                  ),
                  SizedBox(width: 8.0),
                  IconButton(
                    icon: Icon(Icons.send, color: Color(0xFF199A8E)),
                    onPressed: () async {
                      await provider.sendMessage(contactName);
                      // Handle send message
                    },
                  ),
                ],
              ),
            ),
          ],
        ),
      );
    });
  }
}
