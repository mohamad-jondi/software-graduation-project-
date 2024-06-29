import 'dart:developer';

import 'package:flutter/material.dart';
import 'package:flutter_app/App_Router/App_Router.dart';
import 'package:flutter_app/DoctorHomePage.dart';
import 'package:flutter_app/MotherHomePage.dart';
import 'package:flutter_app/NurseHomePage.dart';
import 'package:flutter_app/providers/AppProvider.dart';
import 'package:image_picker/image_picker.dart';
import 'dart:io';

import 'package:provider/provider.dart';

import 'PatientHomePage.dart';

class DoctorSignup extends StatefulWidget {
  @override
  _DoctorProfilePageState createState() => _DoctorProfilePageState();
}

class _DoctorProfilePageState extends State<DoctorSignup> {
  final _formKey = GlobalKey<FormState>();
  final Map<String, dynamic> _formData = {
    'name': '',
    'username': '',
    'phoneNumber': '',
    'email': '',
    'repeat password': '',
    'password': '',
    "personType": 0
  };
  final ImagePicker _picker = ImagePicker();
  XFile? _image;

  Future<void> _pickImage() async {
    final XFile? pickedImage =
        await _picker.pickImage(source: ImageSource.gallery);
    setState(() {
      _image = pickedImage;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Consumer<AppProvider>(builder: (context, provider, x) {
      return Scaffold(
        appBar: AppBar(
          title: Text('Doctor Profile'),
        ),
        body: Padding(
          padding: const EdgeInsets.all(16.0),
          child: SingleChildScrollView(
            child: Form(
              key: _formKey,
              child: Column(
                children: <Widget>[
                  // CircleAvatar(
                  //   radius: 50,
                  //   backgroundImage: _image != null
                  //       ? FileImage(File(_image!.path))
                  //       : AssetImage('images/Person.png') as ImageProvider,
                  // ),
                  // SizedBox(height: 8),
                  // TextButton.icon(
                  //   onPressed: _pickImage,
                  //   icon: Icon(Icons.upload_file),
                  //   label: Text('Upload your picture'),
                  // ),
                  SizedBox(height: 16),
                  _buildProfileField('username', 'username', 'username'),
                  _buildProfileField('name', 'name', 'name'),
                  _buildProfileField(
                      'phoneNumber', 'phoneNumber', 'phoneNumber'),
                  _buildProfileField('email', 'email', 'email'),
                  _buildProfileField('password', 'password', 'password'),
                  _buildProfileField(
                      'repeat password', 'repeat password', 'repeat password'),
                  SizedBox(height: 16),
                  SizedBox(
                    width: double.infinity,
                    child: ElevatedButton(
                      onPressed: () async {
                        if (_formKey.currentState!.validate()) {
                          _formData['personType'] = provider.userType;
                          _formKey.currentState!.save();
                          // Handle form submission
                          final x = await provider.doctorSignup(_formData);
                          log(x.toString());
                          if (x == true) {
                            ScaffoldMessenger.of(context).showSnackBar(
                              SnackBar(
                                  content:
                                      Text('Form submitted successfully!')),
                            );

                            switch (
                                Provider.of<AppProvider>(context, listen: false)
                                    .loggedUser
                                    .personType) {
                              case "Patient":
                                {
                                  await AppRouter.router
                                      .push(PatientHomePage());
                                }
                              case "Doctor":
                                {
                                  AppRouter.router.push(DoctorHomePage());
                                }
                              case "Nurse":
                                {
                                  AppRouter.router.push(NurseHomePage());
                                }
                              case "Mother":
                                {
                                  AppRouter.router.push(MotherHomePage());
                                }
                            }
                          }
                        }
                      },
                      child: Text('Sign Up'),
                    ),
                  ),
                ],
              ),
            ),
          ),
        ),
      );
    });
  }

  Widget _buildProfileField(String label, String hint, String key) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 8.0),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: <Widget>[
          Text(
            label,
            style: TextStyle(
              fontWeight: FontWeight.bold,
              color: Colors.teal,
            ),
          ),
          SizedBox(height: 8),
          TextFormField(
            decoration: InputDecoration(
              filled: true,
              fillColor: Color(0xFF199A8E),
              border: OutlineInputBorder(
                borderRadius: BorderRadius.circular(20.0),
              ),
              hintText: hint,
              hintStyle: TextStyle(color: Colors.white),
            ),
            style: TextStyle(color: Colors.white),
            validator: (value) {
              if (value == null || value.isEmpty) {
                return 'Please enter $hint';
              }
              if (key == 'repeat password' || key == 'password') {
                if (_formData['repeat password'] != _formData['password']) {
                  return 'Passwords not match';
                }
              }
              return null;
            },
            onSaved: (value) {
              _formData[key] = value!;
            },
          ),
        ],
      ),
    );
  }
}
