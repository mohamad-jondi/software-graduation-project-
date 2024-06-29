import 'package:flutter/material.dart';
import 'package:flutter_app/App_Router/App_Router.dart';
import 'package:flutter_app/DoctorDetailPage.dart';

import 'package:flutter_app/models/Doctor.dart';
import 'package:flutter_app/providers/AppProvider.dart';
import 'package:provider/provider.dart';

import '../ApiHandler/API.dart';
import 'ImageNetworkWithFallback.dart';

class DoctorWidget extends StatelessWidget {
  DoctorWidget({
    Key? key,
    required this.doctor,
  }) : super(key: key);
  final Doctor doctor;
  @override
  Widget build(BuildContext context) {
    return Consumer<AppProvider>(builder: (context, provider, x) {
      return InkWell(
        onTap: () async {
          await provider.getUserAvaila(doctor.username!);
          Provider.of<AppProvider>(context, listen: false).selectedDoctor =
              doctor;
          AppRouter.router.push(DoctorDetailPage());
        },
        child: Card(
          shape:
              RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
          child: Padding(
            padding: const EdgeInsets.all(20.0), // Increase padding
            child: Row(
              children: [
                ClipRRect(
                  borderRadius: BorderRadius.circular(60),
                  child: Container(
                      width: 60,
                      height: 60,
                      child: NetworkImageWithFallback(
                        imageUrl:
                            '${API.apis.server}/uploads/${provider.loggedUser.username}.png',
                        fallbackImageUrl:
                            '${API.apis.server}/uploads/DefaulPicture.png',
                      )),
                ),
                SizedBox(width: 20), // Increase space between image and text
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        'Dr. ${doctor.name}',
                        style: TextStyle(
                            fontSize: 22, fontWeight: FontWeight.bold),
                      ),
                      Text(
                        '${doctor.specialization}',
                        style: TextStyle(color: Colors.grey, fontSize: 18),
                      ),
                      SizedBox(height: 5), // Add space between text and rating
                      Row(
                        children: [
                          Icon(Icons.star, color: Colors.amber, size: 20),
                          Text('4.7',
                              style:
                                  TextStyle(color: Colors.grey, fontSize: 18)),
                          SizedBox(width: 10),
                          Text('800m away',
                              style:
                                  TextStyle(color: Colors.grey, fontSize: 18)),
                        ],
                      ),
                    ],
                  ),
                ),
              ],
            ),
          ),
        ),
      );
    });
  }
}
