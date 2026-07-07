import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { TicketService } from '../../core/services/ticket'; 

@Component({
  selector: 'app-create-ticket',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './create-ticket.html',
  styleUrl: './create-ticket.scss'
})
export class CreateTicketComponent {
  fullName = '';
  email = '';
  description = '';
  selectedFile: File | null = null;
  errorMessage = '';

  constructor(private ticketService: TicketService, private router: Router) {}

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files?.length) {
      this.selectedFile = input.files[0];
    }
  }

  onSubmit(): void {
    this.errorMessage = '';
    const formData = new FormData();
    formData.append('FullName', this.fullName);
    formData.append('Email', this.email);
    formData.append('Description', this.description);
    if (this.selectedFile) {
      formData.append('image', this.selectedFile);
    }

this.ticketService.createTicket(formData).subscribe({
  next: (response: { id: string }) => this.router.navigate(['/ticket', response.id]),
  error: (err: any) => this.errorMessage = err.error?.errors
    ? Object.values(err.error.errors).flat().join(', ')
    : 'שגיאה ביצירת הטיקט'
});
  }
}