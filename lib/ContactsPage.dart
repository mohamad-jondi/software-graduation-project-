import 'package:flutter/material.dart';
import 'package:flutter_app/ApiHandler/API.dart';
import 'package:flutter_app/ChatPage.dart';
import 'package:flutter_app/providers/AppProvider.dart';
import 'package:flutter_app/widgets/ImageNetworkWithFallback.dart';
import 'package:provider/provider.dart';

class Contactspage extends StatelessWidget {
  final List<Map<String, dynamic>> contacts = [
    {
      "name": "John Doe",
      "image": "images/doctorimage.png",
      "messages": 5,
    },
    {
      "name": "Jane Smith",
      "image": "images/doctorimage.png",
      "messages": 2,
    },
    {
      "name": "Alice Brown",
      "image": "images/doctorimage.png",
      "messages": 1,
    },
    {
      "name": "Bob Johnson",
      "image": "images/doctorimage.png",
      "messages": 3,
    },
  ];

  @override
  Widget build(BuildContext context) {
    return Consumer<AppProvider>(builder: (context, provider, x) {
      return Scaffold(
        appBar: AppBar(
          title: Text(
            'Your Contacts',
            style: TextStyle(color: Color(0xFF199A8E)),
          ),
          iconTheme: IconThemeData(
              color: Color(0xFF199A8E)), // Optional: Change AppBar icon color
        ),
        body: Padding(
          padding: EdgeInsets.symmetric(horizontal: 16.0),
          child: ListView.builder(
            itemCount: provider.chats.length,
            itemBuilder: (context, index) {
              return Column(
                children: [
                  Container(
                    decoration: BoxDecoration(
                      border: Border.all(color: Color(0xFF199A8E)),
                      borderRadius: BorderRadius.circular(12),
                    ),
                    child: ListTile(
                      leading: ClipRRect(
                        borderRadius: BorderRadius.all(Radius.circular(25)),
                        child: NetworkImageWithFallback(
                            width: 50,
                            height: 50,
                            imageUrl:
                                '${API.apis.server}/uploads/${provider.chats[index].firstPartyUserName == provider.loggedUser.username ? provider.chats[index].secondPartyUsername : provider.chats[index].firstPartyUserName}.png'),
                      ),
                      title: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(provider.chats[index].secondPartyUsername!),
                          Text(
                            provider.chats[index].lastSentMassagess ?? "",
                            style: TextStyle(color: Colors.grey),
                          )
                        ],
                      ),
                      trailing: provider.chats[index].numberOfMessages != 0
                          ? Container(
                              padding: EdgeInsets.symmetric(
                                  vertical: 4.0, horizontal: 8.0),
                              decoration: BoxDecoration(
                                color: Color(0xFF199A8E),
                                borderRadius: BorderRadius.circular(12),
                              ),
                              child: Text(
                                '${provider.chats[index].numberOfMessages}',
                                style: TextStyle(color: Colors.white),
                              ),
                            )
                          : SizedBox(),
                      onTap: () async {
                        await provider
                            .getChatMessages(provider.chats[index].chatId!);
                        await provider
                            .setChatRead(provider.chats[index].chatId!);
                        Navigator.push(
                          context,
                          MaterialPageRoute(
                            builder: (context) => ChatPage(
                              contactName: provider
                                          .chats[index].firstPartyUserName ==
                                      provider.loggedUser.username
                                  ? provider.chats[index].secondPartyUsername!
                                  : provider.chats[index].firstPartyUserName!,
                              contactImage:
                                  '${API.apis.server}/uploads/${provider.chats[index].firstPartyUserName == provider.loggedUser.username ? provider.chats[index].secondPartyUsername : provider.chats[index].firstPartyUserName}.png',
                            ),
                          ),
                        );
                      },
                    ),
                  ),
                  SizedBox(height: 10), // Add space between contacts
                ],
              );
            },
          ),
        ),
      );
    });
  }
}
