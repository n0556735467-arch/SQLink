// import { Injectable } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { Observable } from 'rxjs';
// import { TicketResponseDto, UpdateTicketDto } from '../models/ticket.model';

// @Injectable({ providedIn: 'root' })
// export class TicketService {
//   private readonly apiUrl = 'https://localhost:7103/api/Tickets';

//   constructor(private http: HttpClient) {}

//   createTicket(formData: FormData): Observable<{ id: string }> {
//     return this.http.post<{ id: string }>(this.apiUrl, formData);
//   }

//   getTicketById(id: string): Observable<TicketResponseDto> {
//     return this.http.get<TicketResponseDto>(`${this.apiUrl}/${id}`);
//   }

//   getAllTickets(status?: string, search?: string): Observable<TicketResponseDto[]> {
//     let params: any = {};
//     if (status) params.status = status;
//     if (search) params.search = search;
//     return this.http.get<TicketResponseDto[]>(this.apiUrl, { params });
//   }

//   updateTicket(id: string, dto: UpdateTicketDto): Observable<void> {
//     return this.http.put<void>(`${this.apiUrl}/${id}`, dto);
//   }
// }