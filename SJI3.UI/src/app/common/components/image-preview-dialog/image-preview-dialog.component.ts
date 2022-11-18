import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-image-preview-dialog',
  templateUrl: './image-preview-dialog.component.html',
  styleUrls: ['./image-preview-dialog.component.css'],
})
export class ImagePreviewDialogComponent {
  imageUrl: string | ArrayBuffer | SafeResourceUrl =
    'https://bulma.io/images/placeholders/480x480.png';

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private sanitizer: DomSanitizer
  ) {
    this.imageUrl = this.getImageFromBase64(data.image);
  }

  getImageFromBase64(base64String: string): SafeResourceUrl {
    return this.sanitizer.bypassSecurityTrustResourceUrl(
      'data:image/jpeg;base64,' + base64String
    );
  }
}
