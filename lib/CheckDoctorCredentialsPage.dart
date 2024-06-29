import 'package:flutter/material.dart';
import 'package:flutter_app/providers/AppProvider.dart';
import 'package:provider/provider.dart';

class CheckDoctorCredentialsPage extends StatelessWidget {
  final String doctorName;
  final String doctorImage;
  final String requestDate;
  final String doctorCredentials;

  const CheckDoctorCredentialsPage({
    required this.doctorName,
    required this.doctorImage,
    required this.requestDate,
    required this.doctorCredentials,
  });

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Check Credentials', style: TextStyle(color: Colors.white)),
        backgroundColor: Color(0xFF199A8E),
        iconTheme: IconThemeData(color: Colors.white),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Row(
              children: [
                CircleAvatar(
                  radius: 40,
                  backgroundImage: AssetImage(doctorImage),
                ),
                SizedBox(width: 16),
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      doctorName,
                      style:
                          TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                    ),
                    SizedBox(height: 8),
                    Text(
                      'Request Date: $requestDate',
                      style: TextStyle(fontSize: 16),
                    ),
                  ],
                ),
              ],
            ),
            SizedBox(height: 20),
            Text(
              'Credentials',
              style: TextStyle(
                  fontSize: 22,
                  fontWeight: FontWeight.bold,
                  color: Color(0xFF199A8E)),
            ),
            SizedBox(height: 10),
            Text(
              doctorCredentials,
              style: TextStyle(fontSize: 16),
            ),
            Spacer(),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: [
                ElevatedButton(
                  onPressed: () async {
                    // Handle accept action
                    await Provider.of<AppProvider>(context)
                        .verifyDoctors(doctorName);
                  },
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.green,
                    minimumSize: Size(150, 40),
                  ),
                  child: Text('Accept', style: TextStyle(color: Colors.white)),
                ),
                ElevatedButton(
                  onPressed: () async {
                    // Handle accept action
                    await Provider.of<AppProvider>(context)
                        .rejectDoctor(doctorName);
                  },
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.red,
                    minimumSize: Size(150, 40),
                  ),
                  child: Text('Reject', style: TextStyle(color: Colors.white)),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
