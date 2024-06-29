import 'package:flutter/material.dart';

import '../ApiHandler/API.dart';

class NetworkImageWithFallback extends StatelessWidget {
  final String imageUrl;
   String fallbackImageUrl;
    double width;
    double height;
  NetworkImageWithFallback({
    required this.imageUrl,
     this.fallbackImageUrl='def',
     this.height=100,
     this.width=100
  });

  @override
  Widget build(BuildContext context) {
    if(fallbackImageUrl=='def'){
      fallbackImageUrl='${API.apis.server}/uploads/DefualtPicture.png';
    }
    return Image.network(
      imageUrl,
      fit: BoxFit.fill,
      width: width,
      height: height,
      errorBuilder:
          (BuildContext context, Object error, StackTrace? stackTrace) {
        return Image.network(fallbackImageUrl);
      },
    );
  }
}
