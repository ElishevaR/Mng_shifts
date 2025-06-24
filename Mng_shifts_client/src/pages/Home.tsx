import { useNavigate } from "react-router-dom";

export default function Home() {
  const navigate = useNavigate();

  const handleNavigate = (path: string) => {
    const username = JSON.parse(localStorage.getItem("username") || "null");
    if (!username) {
      navigate("/login");
    } else {
      navigate(`/user/${username}/${path}`);
    }
  };

  return (
    <div>
      <h1>ברוך הבא למערכת ניהול משמרות</h1>
      <nav>
        <ul>
          <li>
            <button onClick={() => handleNavigate("shifts")}>
              הצג משמרות
            </button>
          </li>
          <li>
            <button onClick={() => handleNavigate("requests")}>
              בקשות להחלפה
            </button>
          </li>
          <li>
            <button onClick={() => handleNavigate("swaps")}>
              תהליכי החלפה
            </button>
          </li>
        </ul>
      </nav>
    </div>
  );
}
