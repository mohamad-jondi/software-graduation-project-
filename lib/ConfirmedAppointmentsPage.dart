import 'package:flutter/material.dart';
import 'package:flutter_app/AppointmentPage.dart';
import 'package:flutter_app/providers/AppProvider.dart';
import 'package:provider/provider.dart';

class ConfirmedAppointmentsPage extends StatelessWidget {
  final List<Map<String, String>> confirmedAppointments;

  const ConfirmedAppointmentsPage({required this.confirmedAppointments});

  @override
  Widget build(BuildContext context) {
    return Consumer<AppProvider>(builder: (context, provider, x) {
      return Scaffold(
        appBar: AppBar(
          title: Text('Confirmed Appointments',
              style: TextStyle(color: Colors.white)),
          backgroundColor: Color(0xFF199A8E),
          iconTheme: IconThemeData(color: Colors.white),
        ),
        body: Padding(
          padding: const EdgeInsets.all(16.0),
          child: ListView.builder(
            itemCount: provider.aappointments.length,
            itemBuilder: (context, index) {
              final appointment = provider.aappointments[index].toMap();
              return GestureDetector(
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => AppointmentPage(
                        patientName: appointment['patientName']!,
                        appointmentDate: appointment['date']!,
                        appointmentStatus: appointment['status']!,
                        appointment: provider.aappointments[index],
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
                          appointment['patientName'] ?? 'Unknown',
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
                            color: Colors.green,
                            borderRadius: BorderRadius.circular(12),
                          ),
                          child: Text(
                            'Status: ${appointment['status'] ?? 'Confirmed'}',
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
    });
  }
}
