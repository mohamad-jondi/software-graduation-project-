import 'package:flutter/material.dart';
import 'package:flutter_app/ApiHandler/API.dart';
import 'package:flutter_app/providers/AppProvider.dart';
import 'package:flutter_app/widgets/ImageNetworkWithFallback.dart';
import 'package:provider/provider.dart';

class EditProfileDoctor extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Consumer<AppProvider>(builder: (context, provider, x) {
      return Scaffold(
        appBar: AppBar(
          title: Text('Edit Profile', style: TextStyle(color: Colors.white)),
          backgroundColor: Color(0xFF199A8E),
          iconTheme: IconThemeData(color: Colors.white),
        ),
        body: Column(
          children: [
            SizedBox(
              width: double.infinity,
              child: Container(
                color: Color(0xFF199A8E),
                child: Column(
                  children: [
                    SizedBox(height: 16),
                    ClipRRect(
                      borderRadius: BorderRadius.circular(100),
                      child: Container(
                          width: 100,
                          height: 100,
                          child: NetworkImageWithFallback(
                            imageUrl:
                                '${API.apis.server}/uploads/${provider.loggedUser.username}.png',
                            fallbackImageUrl:
                                '${API.apis.server}/uploads/DefualtPicture.png',
                          )),
                    ),
                    SizedBox(height: 8),
                    TextButton(
                      onPressed: () async {
                        await provider.chooseImage();
                        // Handle change picture action
                      },
                      child: Text(
                        'Change Picture',
                        style: TextStyle(color: Colors.white),
                      ),
                    ),
                    SizedBox(height: 16),
                  ],
                ),
              ),
            ),
            Expanded(
              child: Container(
                decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.only(
                    topLeft: Radius.circular(16.0),
                    topRight: Radius.circular(16.0),
                  ),
                ),
                child: Padding(
                  padding: const EdgeInsets.all(16.0),
                  child: ListView(
                    children: [
                      TextField(
                        controller: provider.usernameController,
                        decoration: InputDecoration(
                          labelText: 'Username',
                          border: OutlineInputBorder(),
                        ),
                      ),
                      SizedBox(height: 16),
                      TextField(
                        controller: provider.nameController,
                        decoration: InputDecoration(
                          labelText: 'Name',
                          border: OutlineInputBorder(),
                        ),
                      ),
                      SizedBox(height: 16),
                      TextField(
                        controller: provider.emailController,
                        decoration: InputDecoration(
                          labelText: 'Email Id',
                          border: OutlineInputBorder(),
                        ),
                      ),
                      SizedBox(height: 16),
                      TextField(
                        controller: provider.phoneController,
                        decoration: InputDecoration(
                          labelText: 'Phone Number',
                          border: OutlineInputBorder(),
                        ),
                      ),
                      SizedBox(height: 16),
                      TextField(
                        controller: provider.passwordController,
                        decoration: InputDecoration(
                          labelText: 'Password',
                          border: OutlineInputBorder(),
                        ),
                      ),
                      SizedBox(height: 16),
                      ElevatedButton(
                        onPressed: () async {
                          await provider.updateUser();
                          // Handle update action
                        },
                        style: ElevatedButton.styleFrom(
                          backgroundColor:
                              Color(0xFF199A8E), // Button background color
                        ),
                        child: Text('Update',
                            style: TextStyle(color: Colors.white)),
                      ),
                    ],
                  ),
                ),
              ),
            ),
          ],
        ),
      );
    });
  }
}
