import 'package:flutter/material.dart';
import 'package:flutter_app/models/AppointmentDTO.dart';

class AppointmentPage extends StatelessWidget {
  final String patientName;
  final String appointmentDate;
  final String appointmentStatus;
  final Appointment appointment;
  const AppointmentPage(
      {required this.patientName,
      required this.appointmentDate,
      required this.appointmentStatus,
      required this.appointment});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title:
            Text('Appointment Details', style: TextStyle(color: Colors.white)),
        backgroundColor: Color(0xFF199A8E),
        iconTheme: IconThemeData(color: Colors.white),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          children: [
            Expanded(
              child: ListView(
                children: [
                  Text(
                    patientName,
                    style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                  ),
                  SizedBox(height: 8),
                  Text(
                    'Date: $appointmentDate',
                    style: TextStyle(fontSize: 18),
                  ),
                  SizedBox(height: 4),
                  Row(
                    children: [
                      Text(
                        'Status: ',
                        style: TextStyle(fontSize: 18),
                      ),
                      Container(
                        padding: EdgeInsets.symmetric(
                            vertical: 4.0, horizontal: 8.0),
                        decoration: BoxDecoration(
                          color: _getStatusColor(appointmentStatus),
                          borderRadius: BorderRadius.circular(12),
                        ),
                        child: Text(
                          appointmentStatus,
                          style: TextStyle(color: Colors.white, fontSize: 18),
                        ),
                      ),
                    ],
                  ),
                  SizedBox(height: 16),
                  _buildDescriptionSection(
                    context,
                    description: appointment.description ?? "",
                  ),
                  SizedBox(height: 16),
                  _buildSection(
                    context,
                    title: 'Allergies',
                    items: ['Pollen', 'Dust', 'Peanuts'], // Dummy data
                  ),
                  SizedBox(height: 16),
                  _buildSection(
                    context,
                    title: 'Medications',
                    items: ['Aspirin', 'Metformin'], // Dummy data
                  ),
                  SizedBox(height: 16),
                  _buildSection(
                    context,
                    title: 'Surgeries',
                    items: ['Appendectomy'], // Dummy data
                  ),
                ],
              ),
            ),
            if (appointmentStatus == 'Pending') ...[
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: [
                  ElevatedButton(
                    onPressed: () {
                      // Handle accept action
                    },
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.green,
                      minimumSize: Size(150, 40), // Increase button width
                    ),
                    child:
                        Text('Accept', style: TextStyle(color: Colors.white)),
                  ),
                  ElevatedButton(
                    onPressed: () {
                      // Handle reject action
                    },
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.red,
                      minimumSize: Size(150, 40), // Increase button width
                    ),
                    child:
                        Text('Reject', style: TextStyle(color: Colors.white)),
                  ),
                ],
              ),
              SizedBox(height: 16),
            ],
          ],
        ),
      ),
    );
  }

  Color _getStatusColor(String status) {
    switch (status) {
      case 'Confirmed':
        return Colors.green;
      case 'Pending':
        return Colors.orange;
      default:
        return Colors.grey;
    }
  }

  Widget _buildDescriptionSection(BuildContext context,
      {required String description}) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          'Description',
          style: TextStyle(
            fontSize: 20,
            fontWeight: FontWeight.bold,
            color: Color(0xFF199A8E),
          ),
        ),
        SizedBox(height: 8),
        Text(description, style: TextStyle(fontSize: 16)),
      ],
    );
  }

  Widget _buildSection(BuildContext context,
      {required String title, required List<String> items}) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          title,
          style: TextStyle(
            fontSize: 20,
            fontWeight: FontWeight.bold,
            color: Color(0xFF199A8E),
          ),
        ),
        ...items.map((item) {
          return Card(
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(12),
            ),
            margin: EdgeInsets.symmetric(vertical: 4),
            child: ListTile(
              title: Text(item),
              trailing: IconButton(
                icon: Icon(Icons.delete, color: Colors.red),
                onPressed: () {
                  _showDeleteConfirmationDialog(context, item);
                },
              ),
            ),
          );
        }).toList(),
        Align(
          alignment: Alignment.centerRight,
          child: ElevatedButton(
            style: ElevatedButton.styleFrom(
              backgroundColor: Color(0xFF199A8E), // Button background color
            ),
            onPressed: () {
              // Handle add action
            },
            child: Text('Add', style: TextStyle(color: Colors.white)),
          ),
        ),
      ],
    );
  }

  void _showDeleteConfirmationDialog(BuildContext context, String item) {
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text('Delete Confirmation'),
          content: Text('Are you sure you want to delete "$item"?'),
          actions: [
            TextButton(
              child: Text('Cancel'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
            TextButton(
              child: Text('Delete', style: TextStyle(color: Colors.red)),
              onPressed: () {
                // Handle delete action here
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }
}
