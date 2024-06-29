import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_app/ApiHandler/API.dart';
import 'package:flutter_app/App_Router/App_Router.dart';
import 'package:flutter_app/models/AppointmentDTO.dart';
import 'package:flutter_app/providers/AppProvider.dart';
import 'package:provider/provider.dart';

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
    return Consumer<AppProvider>(builder: (context, provider, x) {
      return Scaffold(
        appBar: AppBar(
          title: Text('Appointment Details',
              style: TextStyle(color: Colors.white)),
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
                      style:
                          TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
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
                      items: provider.allergies
                          .map((e) => e.allergy ?? "")
                          .toList(), // Dummy data
                    ),
                    SizedBox(height: 16),
                    _buildSection(
                      context,
                      title: 'Medications',
                      items: provider.selectedCase!.drugs!
                          .map((e) => e.name ?? "")
                          .toList(), // Dummy data
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
    });
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
    return Consumer<AppProvider>(builder: (context, provider, x) {
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
                showDialog(
                    context: context,
                    builder: (context) {
                      bool x = title == 'Allergies';
                      String severity = "";
                      String reactionDescription = "";
                      String allergey = "";
                      String drugDosageTime = "";
                      String quantityConsumed = "";
                      String name = "";
                      String duration = "";
                      return AlertDialog(
                        title: Text("Add new " + (x ? "Allergy" : "Medicine")),
                        content: x
                            ? Column(
                                children: [
                                  TextFormField(
                                    decoration: InputDecoration(
                                      labelText: 'allergey',
                                    ),
                                    onChanged: (value) {
                                      allergey = value;
                                    },
                                    validator: (value) {
                                      if (value == null || value.isEmpty) {
                                        return 'Please enter an allergey';
                                      }
                                      return null;
                                    },
                                  ),
                                  TextFormField(
                                    decoration: InputDecoration(
                                      labelText: 'severity',
                                    ),
                                    onChanged: (value) {
                                      severity = value;
                                    },
                                    validator: (value) {
                                      if (value == null || value.isEmpty) {
                                        return 'Please enter a severity';
                                      }
                                      return null;
                                    },
                                  ),
                                  TextFormField(
                                    decoration: InputDecoration(
                                      labelText: 'reaction Description',
                                    ),
                                    onChanged: (value) {
                                      reactionDescription = value;
                                    },
                                    validator: (value) {
                                      if (value == null || value.isEmpty) {
                                        return 'Please enter a reaction Description';
                                      }
                                      return null;
                                    },
                                  ),
                                ],
                              )
                            : Column(
                                children: [
                                  TextFormField(
                                    decoration: InputDecoration(
                                      labelText: 'name',
                                    ),
                                    onChanged: (value) {
                                      name = value;
                                    },
                                    validator: (value) {
                                      if (value == null || value.isEmpty) {
                                        return 'Please enter a name';
                                      }
                                      return null;
                                    },
                                  ),
                                  TextFormField(
                                    decoration: InputDecoration(
                                      labelText: 'duration',
                                    ),
                                    onChanged: (value) {
                                      duration = value;
                                    },
                                    validator: (value) {
                                      if (value == null || value.isEmpty) {
                                        return 'Please enter a duration';
                                      }
                                      return null;
                                    },
                                  ),
                                  TextFormField(
                                    decoration: InputDecoration(
                                      labelText: 'drugDosageTime',
                                    ),
                                    onChanged: (value) {
                                      drugDosageTime = value;
                                    },
                                    keyboardType: TextInputType.number,
                                    validator: (value) {
                                      if (value == null || value.isEmpty) {
                                        return 'Please enter an drug Dosage Time';
                                      }
                                      return null;
                                    },
                                  ),
                                  TextFormField(
                                    decoration: InputDecoration(
                                      labelText: 'quantity Consumed',
                                    ),
                                    onChanged: (value) {
                                      quantityConsumed = value;
                                    },
                                    keyboardType: TextInputType.number,
                                    validator: (value) {
                                      if (value == null || value.isEmpty) {
                                        return 'Please enter an quantity Consumed';
                                      }
                                      return null;
                                    },
                                  ),
                                ],
                              ),
                        actions: [
                          ElevatedButton(
                              onPressed: () async {
                                if (x) {
                                  await API.apis.addAllergy(
                                      provider.selectedCase!.patientUsername ??
                                          "",
                                      <String, dynamic>{
                                        "severity": severity,
                                        "allergey": allergey,
                                        "reactionDescription":
                                            reactionDescription
                                      });
                                  await provider.getAllergy(
                                      provider.selectedCase!.patientUsername ??
                                          "");
                                } else {
                                  final res = await API.apis.checkDDI(
                                      provider.selectedAppointment!.caseID!,
                                      <String, dynamic>{
                                        "drugDosageTime":
                                            int.parse(drugDosageTime),
                                        "quantityConsumed":
                                            int.parse(quantityConsumed),
                                        'name': name,
                                        'duration': duration
                                      });
                                  if (res.statusCode == 200) {
                                    final data = jsonDecode(res.body);
                                    if (data['doesItInteract'] == true) {
                                      showDialog(
                                        context: context,
                                        builder: (context) => AlertDialog(
                                          title: Text("Warning"),
                                          content: Text(
                                              "This medicine has a conflict with ${data['name']}"),
                                          actions: [
                                            ElevatedButton(
                                                onPressed: () =>
                                                    AppRouter.router.pop(),
                                                child: Text("Cancel")),
                                            ElevatedButton(
                                                onPressed: () async {
                                                  final res = await API.apis
                                                      .addMed(
                                                          provider
                                                              .selectedAppointment!
                                                              .caseID!,
                                                          <String, dynamic>{
                                                        "drugDosageTime":
                                                            int.parse(
                                                                drugDosageTime),
                                                        "quantityConsumed":
                                                            int.parse(
                                                                quantityConsumed),
                                                        'name': name,
                                                        'duration': duration
                                                      });
                                                  if (res.statusCode == 200) {
                                                    await provider.getCase(
                                                        provider
                                                            .selectedAppointment!
                                                            .caseID!);
                                                  }
                                                },
                                                child: Text("Add it anyway"))
                                          ],
                                        ),
                                      );
                                    } else {
                                      final res = await API.apis.addMed(
                                          provider.selectedAppointment!.caseID!,
                                          <String, dynamic>{
                                            "drugDosageTime":
                                                int.parse(drugDosageTime),
                                            "quantityConsumed":
                                                int.parse(quantityConsumed),
                                            'name': name,
                                            'duration': duration
                                          });
                                      if (res.statusCode == 200) {
                                        await provider.getCase(provider
                                            .selectedAppointment!.caseID!);
                                      }
                                    }
                                  }
                                }
                              },
                              child: Text("Add"))
                        ],
                      );
                    });
                // Handle add action
              },
              child: Text('Add', style: TextStyle(color: Colors.white)),
            ),
          ),
        ],
      );
    });
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
