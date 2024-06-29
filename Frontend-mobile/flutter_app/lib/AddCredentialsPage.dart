import 'package:flutter/material.dart';
import 'package:image_picker/image_picker.dart';
import 'dart:io';

class AddCredentialsPage extends StatefulWidget {
  @override
  _AddCredentialsPageState createState() => _AddCredentialsPageState();
}

class _AddCredentialsPageState extends State<AddCredentialsPage> {
  int _numCredentials = 0;
  final List<TextEditingController> _controllers = [];
  final List<XFile?> _images = [];

  @override
  void dispose() {
    _controllers.forEach((controller) => controller.dispose());
    super.dispose();
  }

  void _pickImage(int index) async {
    final ImagePicker _picker = ImagePicker();
    final XFile? image = await _picker.pickImage(source: ImageSource.gallery);

    setState(() {
      _images[index] = image;
    });
  }

  void _updateCredentials(int count) {
    setState(() {
      _numCredentials = count;
      if (_controllers.length < count) {
        for (int i = _controllers.length; i < count; i++) {
          _controllers.add(TextEditingController());
          _images.add(null);
        }
      } else {
        _controllers.length = count;
        _images.length = count;
      }
    });
  }

  void _submitCredentials() {
    for (int i = 0; i < _numCredentials; i++) {
      String credentialText = _controllers[i].text;
      XFile? image = _images[i];
      // You can process the credentials and images here
      print('Credential $i: $credentialText');
      if (image != null) {
        print('Image $i: ${image.path}');
      }
    }
    // Add any additional submission logic here
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(content: Text('Credentials submitted successfully!')),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Add Credentials', style: TextStyle(color: Colors.white)),
        backgroundColor: Color(0xFF199A8E),
        iconTheme: IconThemeData(color: Colors.white),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: ListView(
          children: [
            Text(
              'Please fill your credentials in order for us to verify you',
              style: TextStyle(
                color: Color(0xFF199A8E),
                fontSize: 16,
                fontWeight: FontWeight.bold,
              ),
              textAlign: TextAlign.center,
            ),
            SizedBox(height: 16),
            TextField(
              decoration: InputDecoration(
                labelText: 'How many credentials do you have?',
                border: OutlineInputBorder(),
              ),
              keyboardType: TextInputType.number,
              onChanged: (value) {
                int count = int.tryParse(value) ?? 0;
                _updateCredentials(count);
              },
            ),
            SizedBox(height: 16),
            ...List.generate(_numCredentials, (index) {
              return Column(
                children: [
                  TextField(
                    controller: _controllers[index],
                    decoration: InputDecoration(
                      labelText: 'Credential ${index + 1}',
                      border: OutlineInputBorder(),
                    ),
                  ),
                  SizedBox(height: 8),
                  Row(
                    children: [
                      ElevatedButton(
                        onPressed: () => _pickImage(index),
                        style: ElevatedButton.styleFrom(
                          backgroundColor: Color(0xFF199A8E),
                        ),
                        child: Text(
                          'Upload Image',
                          style: TextStyle(color: Colors.white),
                        ),
                      ),
                      SizedBox(width: 16),
                      _images[index] != null
                          ? Image.file(
                              File(_images[index]!.path),
                              width: 100,
                              height: 100,
                            )
                          : Container(),
                    ],
                  ),
                  SizedBox(height: 16),
                ],
              );
            }),
            SizedBox(height: 16),
            ElevatedButton(
              onPressed: _submitCredentials,
              style: ElevatedButton.styleFrom(
                backgroundColor: Color(0xFF199A8E),
              ),
              child: Text(
                'Submit Credentials',
                style: TextStyle(color: Colors.white),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
