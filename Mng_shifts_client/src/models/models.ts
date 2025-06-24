// Shift.ts
export type ShiftType = "Morning" | "Evening" | "Night";
export type ShiftStatus = "Regular" | "OfferedForSwap" | "Swapped"|"SwapRequested";

export interface Shift {
  id: number;
  date: string;
  type: string;
  status: string;
  employeeId: number;
  employee: Employee;
}


export interface Employee {
  id: number;
  fullName: string;
  email: string;
  phoneNumber?: string;
  username: string;
  passwordHash: number;
  shifts?: Shift[];
  swapProposal?: SwapProposal[];
}

export type SwapRequestStatus = "Open" | "Completed" | "Cancelled";

export interface SwapRequest {
  id: number;
  shiftId: number;
  shift: Shift;
  comment: string;
  createdAt: string; 
  status: SwapRequestStatus;
  swapProposals?: SwapProposal[];
}


export type SwapProposalStatus = "WaitingForApproval" | "Approved" | "Rejected";



export interface SwapProposal {
  id: number;
  swapRequestId: number;
  proposedShiftId: number; // השאירי את זה אם צריך עבור שליחה לשרת
  proposedShift: Shift;     // הוסיפי את זה – חובה לתצוגה
  status: SwapProposalStatus;
}

