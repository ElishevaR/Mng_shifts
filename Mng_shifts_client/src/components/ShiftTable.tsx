import type  { Shift } from '../types/Shift';

interface Props {
  shifts: Shift[];
  onActionClick?: (shiftId: number) => void;
  actionLabel?: string;
}

export default function ShiftTable({ shifts, onActionClick, actionLabel }: Props) {
  return (
    <table>
      <thead>
        <tr>
          <th>תאריך</th>
          <th>סוג</th>
          <th>סטטוס</th>
          {actionLabel && <th>פעולה</th>}
        </tr>
      </thead>
      <tbody>
        {shifts.map(shift => (
          <tr key={shift.id}>
            <td>{shift.date}</td>
            <td>{shift.type}</td>
            <td>{shift.status}</td>
            {actionLabel && (
              <td>
                <button onClick={() => onActionClick?.(shift.id)}>{actionLabel}</button>
              </td>
            )}
          </tr>
        ))}
      </tbody>
    </table>
  );
}
