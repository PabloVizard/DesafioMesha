import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  constructor(private snackBar: MatSnackBar) {}

  show(tipoMessagem: string, message: string, action: string = 'Fechar', duration: number = 3000) {
    this.snackBar.open(message, action, 
      { 
      duration, 
      verticalPosition: 'top',
      panelClass: `app-notification-${tipoMessagem}`
     });
  }
}
