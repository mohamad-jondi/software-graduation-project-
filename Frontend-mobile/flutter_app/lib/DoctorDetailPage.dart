import 'package:flutter/material.dart';

class DoctorDetailPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
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
                Container(
                  width: 100,
                  height: 100,
                  decoration: BoxDecoration(
                    borderRadius: BorderRadius.circular(12),
                    image: DecorationImage(
                      image: AssetImage(
                          'images/doctorimage.png'), // Replace with your asset
                      fit: BoxFit.cover,
                    ),
                  ),
                ),
                SizedBox(width: 16),
                Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      'Dr. Marcus Horizon',
                      style:
                          TextStyle(fontSize: 22, fontWeight: FontWeight.bold),
                    ),
                    Text(
                      'Cardiologist',
                      style: TextStyle(color: Colors.grey, fontSize: 16),
                    ),
                    Row(
                      children: [
                        Icon(Icons.star, color: Colors.amber, size: 20),
                        Text('4.7',
                            style: TextStyle(color: Colors.grey, fontSize: 16)),
                        SizedBox(width: 10),
                        Icon(Icons.location_on, color: Colors.green, size: 20),
                        Text('800m away',
                            style: TextStyle(color: Colors.grey, fontSize: 16)),
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
                  _buildDateChip('Mon', '21', false),
                  SizedBox(width: 8),
                  _buildDateChip('Tue', '22', false),
                  SizedBox(width: 8),
                  _buildDateChip('Wed', '23', true),
                  SizedBox(width: 8),
                  _buildDateChip('Thu', '24', false),
                  SizedBox(width: 8),
                  _buildDateChip('Fri', '25', false),
                  SizedBox(width: 8),
                  _buildDateChip('Sat', '26', false),
                ],
              ),
            ),
            SizedBox(height: 20),
            Wrap(
              spacing: 10,
              runSpacing: 10,
              children: [
                _buildTimeChip('09:00 AM', false),
                _buildTimeChip('10:00 AM', false),
                _buildTimeChip('11:00 AM', false),
                _buildTimeChip('01:00 PM', false),
                _buildTimeChip('02:00 PM', true),
                _buildTimeChip('03:00 PM', false),
                _buildTimeChip('04:00 PM', false),
                _buildTimeChip('07:00 PM', false),
                _buildTimeChip('08:00 PM', false),
              ],
            ),
            SizedBox(height: 30),
            Text(
              'Description',
              style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
            ),
            SizedBox(height: 10),
            TextField(
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
                    onPressed: () {
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
                    onPressed: () {
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
  }

  Widget _buildDateChip(String day, String date, bool isSelected) {
    return ChoiceChip(
      label: Column(
        children: [
          Text(day, style: TextStyle(color: Colors.grey)),
          Text(date, style: TextStyle(fontWeight: FontWeight.bold)),
        ],
      ),
      selected: isSelected,
      onSelected: (bool selected) {
        // Handle date selection
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

  Widget _buildTimeChip(String time, bool isSelected) {
    return ChoiceChip(
      label: Text(time),
      selected: isSelected,
      onSelected: (bool selected) {
        // Handle time selection
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
