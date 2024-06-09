import 'package:flutter/material.dart';
import 'package:flutter_app/ChatPage.dart';

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
          itemCount: contacts.length,
          itemBuilder: (context, index) {
            final contact = contacts[index];
            return Column(
              children: [
                Container(
                  decoration: BoxDecoration(
                    border: Border.all(color: Color(0xFF199A8E)),
                    borderRadius: BorderRadius.circular(12),
                  ),
                  child: ListTile(
                    leading: CircleAvatar(
                      backgroundImage: AssetImage(contact['image']),
                      radius: 25,
                    ),
                    title: Text(contact['name']),
                    trailing: Container(
                      padding:
                          EdgeInsets.symmetric(vertical: 4.0, horizontal: 8.0),
                      decoration: BoxDecoration(
                        color: Color(0xFF199A8E),
                        borderRadius: BorderRadius.circular(12),
                      ),
                      child: Text(
                        '${contact['messages']}',
                        style: TextStyle(color: Colors.white),
                      ),
                    ),
                    onTap: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) => ChatPage(
                            contactName: contact['name'],
                            contactImage: contact['image'],
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
  }
}
