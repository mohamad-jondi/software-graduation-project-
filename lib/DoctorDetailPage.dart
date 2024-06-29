import 'package:flutter/material.dart';
import 'package:flutter_app/App_Router/App_Router.dart';
import 'package:flutter_app/ChatPage.dart';
import 'package:flutter_app/providers/AppProvider.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';

import 'ApiHandler/API.dart';
import 'widgets/ImageNetworkWithFallback.dart';

class DoctorDetailPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Consumer<AppProvider>(builder: (context, provider, x) {
      return Scaffold(
        appBar: AppBar(
          title: Text(
            'Doctor Detail',
            style: TextStyle(color: Colors.black),
          ),
          iconTheme: IconThemeData(color: Colors.black),
          actions: [
            IconButton(
              icon: Icon(Icons.more_vert, color: Colors.black),
              onPressed: () {
                // Handle more options
              },
            ),
          ],
        ),
        body: Padding(
          padding: const EdgeInsets.all(16.0),
          child: ListView(
            children: [
              Row(
                children: [
                  ClipRRect(
                    borderRadius: BorderRadius.circular(12),
                    child: Container(
                        width: 100,
                        height: 100,
                        child: NetworkImageWithFallback(
                          imageUrl:
                              '${API.apis.server}/uploads/${provider.selectedDoctor.username}.png',
                          fallbackImageUrl:
                              '${API.apis.server}/uploads/DefualtPicture.png',
                        )),
                  ),
                  SizedBox(width: 16),
                  Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        'Dr. ${provider.selectedDoctor.name}',
                        style: TextStyle(
                            fontSize: 22, fontWeight: FontWeight.bold),
                      ),
                      Text(
                        provider.selectedDoctor.specialization ?? "",
                        style: TextStyle(color: Colors.grey, fontSize: 16),
                      ),
                      Row(
                        children: [
                          Icon(Icons.star, color: Colors.amber, size: 20),
                          Text('4.7',
                              style:
                                  TextStyle(color: Colors.grey, fontSize: 16)),
                          SizedBox(width: 10),
                          Icon(Icons.location_on,
                              color: Colors.green, size: 20),
                          Text('800m away',
                              style:
                                  TextStyle(color: Colors.grey, fontSize: 16)),
                        ],
                      ),
                    ],
                  ),
                ],
              ),
              SizedBox(height: 30),
              Text(
                'About',
                style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
              ),
              SizedBox(height: 10),
              Text(
                'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam...',
                style: TextStyle(color: Colors.grey, fontSize: 16),
              ),
              SizedBox(height: 30),
              Text(
                'Availability',
                style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
              ),
              SizedBox(height: 10),
              SingleChildScrollView(
                scrollDirection: Axis.horizontal,
                child: Row(
                  children: [
                    for (var i = 1; i < 8; i++) ...[
                      _buildDateChip(
                          DateFormat('EEEE')
                              .format(DateTime.now().add(Duration(days: i)))
                              .substring(0, 3),
                          DateTime.now().add(Duration(days: i)).day.toString(),
                          provider.chosenDay == i, () {
                        provider.setChosenDay(i);
                      })
                    ],

                    // _buildDateChip('Mon', '21', false),
                    // SizedBox(width: 8),
                    // _buildDateChip('Tue', '22', false),
                    // SizedBox(width: 8),
                    // _buildDateChip('Wed', '23', true),
                    // SizedBox(width: 8),
                    // _buildDateChip('Thu', '24', false),
                    // SizedBox(width: 8),
                    // _buildDateChip('Fri', '25', false),
                    // SizedBox(width: 8),
                    // _buildDateChip('Sat', '26', false),
                  ],
                ),
              ),
              SizedBox(height: 20),
              Wrap(
                spacing: 10,
                runSpacing: 10,
                children: [
                  for (int i = provider.start; i < provider.end; i++) ...[
                    if (provider.aps
                            .where((e) => (DateTime(
                                  DateTime.now().year,
                                  DateTime.now().month,
                                  DateTime.now().day,
                                  i,
                                  0,
                                )
                                    .add(Duration(days: provider.chosenDay))
                                    .compareTo(e.date!) ==
                                0))
                            .length ==
                        0)
                      _buildTimeChip('0' + i.toString() + ":00",
                          '0' + i.toString() + ":00" == provider.chosenTime,
                          () {
                        provider.choosteTime('0' + i.toString() + ":00");
                      }),
                  ],
                  // _buildTimeChip('09:00 AM', false),
                  // _buildTimeChip('10:00 AM', false),
                  // _buildTimeChip('11:00 AM', false),
                  // _buildTimeChip('01:00 PM', false),
                  // _buildTimeChip('02:00 PM', true),
                  // _buildTimeChip('03:00 PM', false),
                  // _buildTimeChip('04:00 PM', false),
                  // _buildTimeChip('07:00 PM', false),
                  // _buildTimeChip('08:00 PM', false),
                ],
              ),
              SizedBox(height: 30),
              Text(
                'Description',
                style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
              ),
              SizedBox(height: 10),
              TextField(
                controller: provider.notesController,
                maxLines: 5,
                decoration: InputDecoration(
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.circular(12),
                  ),
                  hintText: 'Enter additional information...',
                ),
              ),
              SizedBox(height: 30),
              Align(
                alignment: Alignment.center,
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    ElevatedButton(
                      onPressed: () async {
                        await provider
                            .figureChat(provider.selectedDoctor.username!);
                        AppRouter.router.push(ChatPage(
                            contactName: provider.selectedDoctor.username!,
                            contactImage:
                                '${API.apis.server}/uploads/${provider.selectedDoctor.username}.png'));
                        // Handle chat action
                      },
                      style: ElevatedButton.styleFrom(
                        backgroundColor: Colors.grey[300], // Background color
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(30.0),
                        ),
                        padding:
                            EdgeInsets.symmetric(horizontal: 20, vertical: 15),
                      ),
                      child: Icon(Icons.chat,
                          color: Color(0xFF199A8E)), // Icon color
                    ),
                    SizedBox(width: 16),
                    ElevatedButton.icon(
                      onPressed: () async {
                        await provider.reqApp();
                        // Handle booking action
                      },
                      icon: Icon(Icons.calendar_today, color: Colors.white),
                      label: Text('Book Appointment',
                          style: TextStyle(color: Colors.white)), // Text color
                      style: ElevatedButton.styleFrom(
                        backgroundColor: Color(0xFF199A8E),
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(30.0),
                        ),
                        padding:
                            EdgeInsets.symmetric(horizontal: 30, vertical: 15),
                        textStyle: TextStyle(fontSize: 18),
                      ),
                    ),
                  ],
                ),
              ),
            ],
          ),
        ),
      );
    });
  }

  Widget _buildDateChip(
      String day, String date, bool isSelected, Function onSelect) {
    return ChoiceChip(
      label: Column(
        children: [
          Text(day, style: TextStyle(color: Colors.grey)),
          Text(date, style: TextStyle(fontWeight: FontWeight.bold)),
        ],
      ),
      selected: isSelected,
      onSelected: (bool selected) {
        onSelect();
      },
      selectedColor: Color(0xFF199A8E),
      backgroundColor: Colors.white,
      labelStyle: TextStyle(color: isSelected ? Colors.white : Colors.black),
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(8),
        side: BorderSide(color: Colors.grey),
      ),
      padding: EdgeInsets.symmetric(vertical: 10, horizontal: 12),
    );
  }

  Widget _buildTimeChip(String time, bool isSelected, Function onSelect) {
    return ChoiceChip(
      label: Text(time),
      selected: isSelected,
      onSelected: (bool selected) {
        onSelect();
      },
      selectedColor: Color(0xFF199A8E),
      backgroundColor: Colors.white,
      labelStyle: TextStyle(color: isSelected ? Colors.white : Colors.black),
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(8),
        side: BorderSide(color: Colors.grey),
      ),
      padding: EdgeInsets.symmetric(vertical: 10, horizontal: 12),
    );
  }
}
