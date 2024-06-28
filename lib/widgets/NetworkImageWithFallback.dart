import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

import '../ApiHandler/API.dart';

class NetworkImageWithFallback extends StatelessWidget {
  final String imageUrl;
  String fallbackImageUrl;

  NetworkImageWithFallback({
    required this.imageUrl,
    this.fallbackImageUrl = 'def',
  });

  Future<bool> _imageExists(String url) async {
    try {
      final response = await http.head(Uri.parse(url));
      return response.statusCode == 200;
    } catch (e) {
      return false;
    }
  }

  @override
  Widget build(BuildContext context) {
    if (fallbackImageUrl == 'def') {
      fallbackImageUrl = '${API.apis.server}/uploads/DefualtPicture.png';
    }
    return FutureBuilder<bool>(
      future: _imageExists(imageUrl),
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.done) {
          final imageExists = snapshot.data ?? false;
          return Image(
            image: NetworkImage(imageExists ? imageUrl : fallbackImageUrl),
          );
        } else {
          return CircularProgressIndicator();
        }
      },
    );
  }
}
