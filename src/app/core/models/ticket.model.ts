export interface CreateTicketDto {
  fullName: string;
  email: string;
  description: string;
}

export interface UpdateTicketDto {
  status: string;
  resolutionText: string | null;
}

export interface TicketResponseDto {
  id: string;
  fullName: string;
  email: string;
  description: string;
  imagePath: string | null;
  status: string;
  resolutionText: string | null;
  createdAt: string;
  updatedAt: string;
  lastUpdatedBy: string | null;
}

export interface MockLoginDto {
  email: string;
  role: string;
}