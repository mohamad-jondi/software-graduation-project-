import 'package:flutter/material.dart';

class PatientHomeScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: ListView(
          children: [
            SizedBox(height: 10),
            TextField(
              decoration: InputDecoration(
                hintText: 'Find a doctor',
                prefixIcon: Icon(Icons.search),
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(30),
                  borderSide: BorderSide.none,
                ),
                filled: true,
                fillColor: Colors.grey[200],
              ),
            ),
            SizedBox(height: 10),
            Text(
              'Category',
              style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
            ),
            SizedBox(height: 10),
            Wrap(
              spacing: 10,
              runSpacing: 10,
              children: [
                _buildCategoryItem(context, Icons.local_hospital, 'General'),
                _buildCategoryItem(
                    context, Icons.pregnant_woman, 'Lungs Specialist'),
                _buildCategoryItem(context, Icons.medical_services, 'Dentist'),
                _buildCategoryItem(context, Icons.psychology, 'Psychiatrist'),
                _buildCategoryItem(context, Icons.coronavirus, 'Covid-19'),
                _buildCategoryItem(context, Icons.healing, 'Surgeon'),
                _buildCategoryItem(context, Icons.favorite, 'Cardiologist'),
              ],
            ),
            SizedBox(height: 20),
            Text(
              'Recommended Doctors',
              style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
            ),
            SizedBox(height: 10),
            _buildRecommendedDoctor(),
            SizedBox(height: 20),
            Text(
              'Your Recent Doctors',
              style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
            ),
            SizedBox(height: 10),
            _buildRecentDoctors(),
          ],
        ),
      ),
    );
  }

  Widget _buildCategoryItem(BuildContext context, IconData icon, String label) {
    return Container(
      width: (MediaQuery.of(context).size.width - 64) /
          4, // Adjusting width to fit 4 items in a row
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          CircleAvatar(
            radius: 35, // Increase the radius for a larger icon
            backgroundColor: Colors.grey[200],
            child: Icon(icon,
                size: 40, color: Color(0xFF199A8E)), // Increase icon size
          ),
          SizedBox(height: 5),
          Text(label,
              style: TextStyle(fontSize: 12), textAlign: TextAlign.center),
        ],
      ),
    );
  }

  Widget _buildRecommendedDoctor() {
    return Card(
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
      child: Padding(
        padding: const EdgeInsets.all(20.0), // Increase padding
        child: Row(
          children: [
            CircleAvatar(
              radius: 60, // Increase the radius for a larger image
              backgroundImage: AssetImage(
                  'images/doctorimage.png'), // Use doctorimage.png for all doctors
            ),
            SizedBox(width: 20), // Increase space between image and text
            Expanded(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    'Dr. Marcus Horizon',
                    style: TextStyle(fontSize: 22, fontWeight: FontWeight.bold),
                  ),
                  Text(
                    'Cardiologist',
                    style: TextStyle(color: Colors.grey, fontSize: 18),
                  ),
                  SizedBox(height: 5), // Add space between text and rating
                  Row(
                    children: [
                      Icon(Icons.star, color: Colors.amber, size: 20),
                      Text('4.7',
                          style: TextStyle(color: Colors.grey, fontSize: 18)),
                      SizedBox(width: 10),
                      Text('800m away',
                          style: TextStyle(color: Colors.grey, fontSize: 18)),
                    ],
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildRecentDoctors() {
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceAround,
      children: [
        _buildRecentDoctorItem('Dr. Marcus',
            'images/doctorimage.png'), // Use doctorimage.png for all doctors
        _buildRecentDoctorItem('Dr. Maria',
            'images/doctorimage.png'), // Use doctorimage.png for all doctors
        _buildRecentDoctorItem('Dr. Stevi',
            'images/doctorimage.png'), // Use doctorimage.png for all doctors
        _buildRecentDoctorItem('Dr. Luke',
            'images/doctorimage.png'), // Use doctorimage.png for all doctors
      ],
    );
  }

  Widget _buildRecentDoctorItem(String name, String imagePath) {
    return Column(
      children: [
        CircleAvatar(
          radius: 30,
          backgroundImage: AssetImage(imagePath),
        ),
        SizedBox(height: 5),
        Text(name, style: TextStyle(fontSize: 12)),
      ],
    );
  }
}
