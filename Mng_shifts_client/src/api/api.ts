import {type Shift,type SwapRequest,type Employee } from "../models/models";

const BASE_URL = "https://localhost:7039/api";
// 🟩 LOGIN
export async function login(username: string, password: string): Promise<Employee | null> {
    console.log("loginloginlogin");
    
    const res = await fetch(`${BASE_URL}/ShiftExchange/employee/login`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ username, password }),
    });
  
    if (!res.ok) return null;
    return await res.json();
  }
  
// 🟩 SHIFT
export async function getShiftsByEmployee(employeeId: number): Promise<Shift[]> {
  const res = await fetch(`${BASE_URL}/ShiftExchange/employee/${employeeId}/shifts`);
  if (!res.ok) throw new Error("שגיאה בשליפת משמרות");
  return await res.json();
}

// 🟩 EMPLOYEE
export async function getEmployeeById(employeeId: number): Promise<Employee> {
  const res = await fetch(`${BASE_URL}/Employee/${employeeId}`);
  if (!res.ok) throw new Error("שגיאה בשליפת משתמש");
  return await res.json();
}

// 🟩 SWAP REQUESTS
export async function getOpenSwapRequests(excludeEmployeeId: number): Promise<SwapRequest[]> {
  const res = await fetch(`${BASE_URL}/ShiftExchange/open-requests/${excludeEmployeeId}`);
  if (!res.ok) throw new Error("שגיאה בשליפת בקשות פתוחות");
  return await res.json();
}

export async function createSwapRequest(shiftId: number): Promise<void> {
  const res = await fetch(`${BASE_URL}/ShiftExchange/request/${shiftId}`, {
    method: "POST",
  });
  if (!res.ok) throw new Error("שגיאה ביצירת בקשת החלפה");
}

export async function createSwapProposal(requestId: number, proposedShiftId: number): Promise<void> {
  const res = await fetch(
    `https://localhost:7039/api/ShiftExchange/propose?requestId=${requestId}&proposedShiftId=${proposedShiftId}`,
    {
      method: "POST",
    }
  );

  if (!res.ok) throw new Error("שגיאה בשליחת הצעת ההחלפה");
}
export async function getSwapRequestsByEmployee(employeeId: number): Promise<SwapRequest[]> {
  const res = await fetch(`https://localhost:7039/api/ShiftExchange/my-swaps/${employeeId}`);
  if (!res.ok) throw new Error("Failed to fetch swap requests");
  return await res.json();
}



export async function respondToProposal(proposalId: number, action: "approve" | "reject"): Promise<void> {
  console.log("respondToProposal in client");
  console.log("proposalId:",proposalId,"action:", action)
  console.log(`https://localhost:7039/api/ShiftExchange/proposals/${proposalId}/respond-proposal?action=${action}`)
  const res = await fetch(`https://localhost:7039/api/ShiftExchange/proposals/${proposalId}/respond-proposal?action=${action}`, {
    method: "PUT",
  });
  if (!res.ok) throw new Error("Failed to respond to proposal");
}


