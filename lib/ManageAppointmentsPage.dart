import 'package:flutter/material.dart';
import 'package:flutter_app/AppointmentPage.dart';
import 'package:flutter_app/providers/AppProvider.dart';
import 'package:provider/provider.dart';

class ManageAppointmentsPage extends StatelessWidget {
  final List<Map<String, String>> pendingAppointments;

  const ManageAppointmentsPage({required this.pendingAppointments});

  @override
  Widget build(BuildContext context) {
    return Consumer<AppProvider>(builder: (context, provider, x) {
      return Scaffold(
        appBar: AppBar(
          title: Text('Manage Appointments',
              style: TextStyle(color: Colors.white)),
          backgroundColor: Color(0xFF199A8E),
          iconTheme: IconThemeData(color: Colors.white),
        ),
        body: Padding(
          padding: const EdgeInsets.all(16.0),
          child: ListView.builder(
            itemCount: provider.pappointments.length,
            itemBuilder: (context, index) {
              final appointment = provider.pappointments[index].toMap();
              return GestureDetector(
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => AppointmentPage(
                        patientName: appointment['patientName']!,
                        appointmentDate: appointment['date']!,
                        appointmentStatus: appointment['status']!,
                        appointment: provider.pappointments[index],
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
                        Row(
                          mainAxisAlignment: MainAxisAlignment.spaceBetween,
                          children: [
                            Text(
                                'Status: ${appointment['status'] ?? 'Pending'}'),
                            Row(
                              children: [
                                ElevatedButton(
                                  onPressed: () async {
                                    await provider.changeApStatus(
                                        appointment['appointmentId'], 1);
                                    // Handle accept action
                                  },
                                  style: ElevatedButton.styleFrom(
                                    backgroundColor: Colors.green,
                                  ),
                                  child: Text('Accept',
                                      style: TextStyle(color: Colors.white)),
                                ),
                                SizedBox(width: 8),
                                ElevatedButton(
                                  onPressed: () async {
                                    await provider.changeApStatus(
                                        appointment['appointmentId'], 4);
                                    // Handle reject action
                                  },
                                  style: ElevatedButton.styleFrom(
                                    backgroundColor: Colors.red,
                                  ),
                                  child: Text('Reject',
                                      style: TextStyle(color: Colors.white)),
                                ),
                              ],
                            ),
                          ],
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
