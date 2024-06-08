import 'package:flutter/material.dart';
import 'package:flutter_app/AppointmentPage.dart';

class PatientAppointmentsPage extends StatelessWidget {
  final List<Map<String, String>> appointments = [
    {
      "patient": "John Doe",
      "time": "10:00 AM",
      "date": "2023-05-01",
      "day": "Monday",
      "status": "Confirmed"
    },
    {
      "patient": "Jane Smith",
      "time": "11:30 AM",
      "date": "2023-05-01",
      "day": "Monday",
      "status": "Confirmed"
    },
    {
      "patient": "Alice Brown",
      "time": "Pending",
      "date": "2023-05-02",
      "day": "Tuesday",
      "status": "Pending"
    },
    {
      "patient": "Bob Johnson",
      "time": "Pending",
      "date": "2023-05-02",
      "day": "Tuesday",
      "status": "Pending"
    },
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Appointments', style: TextStyle(color: Colors.white)),
        backgroundColor: Color(0xFF199A8E),
        iconTheme: IconThemeData(color: Colors.white),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: ListView.builder(
          itemCount: appointments.length,
          itemBuilder: (context, index) {
            final appointment = appointments[index];
            return GestureDetector(
              onTap: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => AppointmentPage(
                      patientName: appointment['patient']!,
                      appointmentDate: appointment['date']!,
                      appointmentStatus: appointment['status']!,
                    ),
                  ),
                );
              },
              child: Card(
                margin: EdgeInsets.symmetric(vertical: 8.0),
                child: Padding(
                  padding: const EdgeInsets.all(16.0),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        appointment['patient'] ?? 'Unknown',
                        style: TextStyle(
                            fontSize: 18, fontWeight: FontWeight.bold),
                      ),
                      SizedBox(height: 8),
                      Text('Date: ${appointment['date'] ?? 'N/A'}'),
                      SizedBox(height: 4),
                      Container(
                        padding: EdgeInsets.symmetric(
                            vertical: 4.0, horizontal: 8.0),
                        decoration: BoxDecoration(
                          color: _getStatusColor(appointment['status']),
                          borderRadius: BorderRadius.circular(12),
                        ),
                        child: Text(
                          'Status: ${appointment['status'] ?? 'N/A'}',
                          style: TextStyle(color: Colors.white),
                        ),
                      ),
                    ],
                  ),
                ),
              ),
            );
          },
        ),
      ),
    );
  }

  Color _getStatusColor(String? status) {
    switch (status) {
      case 'Confirmed':
        return Colors.green;
      case 'Pending':
        return Colors.orange;
      default:
        return Colors.grey;
    }
  }
}
