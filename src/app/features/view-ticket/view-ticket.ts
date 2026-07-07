import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { TicketService } from '../../core/services/ticket';
import { TicketResponseDto } from '../../core/models/ticket.model';

@Component({
  selector: 'app-view-ticket',
  standalone: true,
  imports: [CommonModule, DatePipe],
  templateUrl: './view-ticket.html',
  // styleUrl: './view-ticket.scss'
})

export class ViewTicket implements OnInit {
  ticket: TicketResponseDto | null = null;
  errorMessage = '';

  constructor(
    private route: ActivatedRoute,
    private ticketService: TicketService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) {
      this.errorMessage = 'מזהה טיקט חסר';
      return;
    }

    this.ticketService.getTicketById(id).subscribe({
      next: (data) => this.ticket = data,
      error: () => this.errorMessage = 'הטיקט לא נמצא'
    });
  }
}