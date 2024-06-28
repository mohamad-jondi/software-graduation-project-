import 'package:flutter/material.dart';

class PatientDetailsPage extends StatefulWidget {
  final String patientName;
  final String nextCheckInDate;
  String checkInStatus;

  PatientDetailsPage({
    required this.patientName,
    required this.nextCheckInDate,
    required this.checkInStatus,
  });

  @override
  _PatientDetailsPageState createState() => _PatientDetailsPageState();
}

class _PatientDetailsPageState extends State<PatientDetailsPage> {
  void _markAsComplete() {
    setState(() {
      widget.checkInStatus = 'Completed';
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Patient Details', style: TextStyle(color: Colors.white)),
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
                    widget.patientName,
                    style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                  ),
                  SizedBox(height: 8),
                  Text(
                    'Next Check-In Date: ${widget.nextCheckInDate}',
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
                          color: _getStatusColor(widget.checkInStatus),
                          borderRadius: BorderRadius.circular(12),
                        ),
                        child: Text(
                          widget.checkInStatus,
                          style: TextStyle(color: Colors.white, fontSize: 18),
                        ),
                      ),
                    ],
                  ),
                  SizedBox(height: 16),
                  _buildDescriptionSection(
                    context,
                    description:
                        'This section can contain additional details about the patient or notes from the nurse.',
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
            if (widget.checkInStatus == 'Not Completed')
              ElevatedButton(
                onPressed: _markAsComplete,
                style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.green,
                  minimumSize:
                      Size(double.infinity, 50), // Make button fill the width
                ),
                child: Text('Mark as Complete',
                    style: TextStyle(color: Colors.white)),
              ),
          ],
        ),
      ),
    );
  }

  Color _getStatusColor(String status) {
    switch (status) {
      case 'Completed':
        return Colors.green;
      case 'Not Completed':
        return Colors.red;
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
