// chat_page.dart
import 'package:flutter/material.dart';

class Contactspage extends StatelessWidget {
  final List<Map<String, dynamic>> contacts = [
    {
      "name": "John Doe",
      "image": "assets/john_doe.jpg",
      "messages": 5,
    },
    {
      "name": "Jane Smith",
      "image": "assets/jane_smith.jpg",
      "messages": 2,
    },
    {
      "name": "Alice Brown",
      "image": "assets/alice_brown.jpg",
      "messages": 1,
    },
    {
      "name": "Bob Johnson",
      "image": "assets/bob_johnson.jpg",
      "messages": 3,
    },
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Your Contacts'),
      ),
      body: ListView.builder(
        itemCount: contacts.length,
        itemBuilder: (context, index) {
          final contact = contacts[index];
          return Column(
            children: [
              ListTile(
                leading: CircleAvatar(
                  backgroundImage: AssetImage(contact['image']),
                  radius: 25,
                ),
                title: Text(contact['name']),
                trailing: Container(
                  padding: EdgeInsets.symmetric(vertical: 4.0, horizontal: 8.0),
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
                  // Navigate to chat detail page (to be implemented)
                },
              ),
              SizedBox(height: 10), // Add space between contacts
            ],
          );
        },
      ),
    );
  }
}
