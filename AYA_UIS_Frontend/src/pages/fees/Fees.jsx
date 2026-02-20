import React, { useEffect, useState } from 'react';
import { FiDollarSign, FiCalendar, FiGrid } from 'react-icons/fi';
import { feeService, userStudyYearService } from '../../services/otherServices';
import { useAuth } from '../../contexts/AuthContext';
import { LEVEL_LABELS } from '../../constants';
import { toast } from 'react-toastify';

const FEE_TYPE_LABELS = { Academic: 'Academic', Registration: 'Registration' };

export default function Fees() {
  const { isAdmin } = useAuth();

  // Admin: pick a study year to view all fees
  // Student: auto-load their current study year + department fees

  if (isAdmin) return <AdminFees />;
  return <StudentFees />;
}

/* ─── Admin Fees ─── */
function AdminFees() {
  // Admin sees fees through study-years page, but this route
  // shows a simple search-by-study-year-id view as well
  const [studyYearId, setStudyYearId] = useState('');
  const [fees, setFees] = useState([]);
  const [loading, setLoading] = useState(false);
  const [searched, setSearched] = useState(false);

  const search = async () => {
    if (!studyYearId) return;
    setLoading(true);
    setSearched(true);
    try {
      const res = await feeService.getByStudyYear(studyYearId);
      setFees(res?.data || res || []);
    } catch (e) {
      toast.error('Failed to load fees');
      setFees([]);
    }
    setLoading(false);
  };

  const grouped = fees.reduce((acc, f) => {
    const key = f.departmentName || 'General';
    if (!acc[key]) acc[key] = [];
    acc[key].push(f);
    return acc;
  }, {});

  return (
    <div className="page-container">
      <div className="page-header">
        <h1>
          <FiDollarSign style={{ marginRight: 8 }} />
          Fee Records
        </h1>
        <p>Search fees by study year</p>
      </div>

      <div className="card" style={{ marginBottom: 24 }}>
        <div className="form-row" style={{ alignItems: 'end' }}>
          <div className="form-group" style={{ marginBottom: 0 }}>
            <label>Study Year ID</label>
            <input
              type="number"
              className="form-control"
              min={1}
              value={studyYearId}
              onChange={e => setStudyYearId(e.target.value)}
              placeholder="Enter study year ID"
            />
          </div>
          <div className="form-group" style={{ marginBottom: 0 }}>
            <button
              className="btn btn-primary"
              onClick={search}
              disabled={loading || !studyYearId}
            >
              {loading ? 'Loading...' : 'Search'}
            </button>
          </div>
        </div>
      </div>

      {loading && <div className="spinner" />}

      {!loading && searched && fees.length === 0 && (
        <div className="card empty-state">
          <h3>No fees found</h3>
          <p>No fees configured for this study year.</p>
        </div>
      )}

      {!loading && fees.length > 0 && (
        <div style={{ display: 'grid', gap: 20 }}>
          {Object.entries(grouped).map(([deptName, deptFees]) => (
            <div className="card" key={deptName}>
              <div
                style={{
                  display: 'flex',
                  justifyContent: 'space-between',
                  alignItems: 'center',
                  marginBottom: 16,
                }}
              >
                <h3>{deptName}</h3>
                <span className="badge badge-info">
                  {deptFees.length} fee{deptFees.length !== 1 ? 's' : ''}
                </span>
              </div>
              <table>
                <thead>
                  <tr>
                    <th>Type</th>
                    <th>Level</th>
                    <th>Description</th>
                    <th>Amount</th>
                  </tr>
                </thead>
                <tbody>
                  {deptFees.map(f => (
                    <tr key={f.id}>
                      <td>
                        <span
                          className={`badge ${f.type === 'Academic' ? 'badge-info' : 'badge-warning'}`}
                        >
                          {FEE_TYPE_LABELS[f.type] || f.type}
                        </span>
                      </td>
                      <td>{LEVEL_LABELS[f.level] || f.level}</td>
                      <td>{f.description || '—'}</td>
                      <td>
                        <strong>${f.amount?.toLocaleString()}</strong>
                      </td>
                    </tr>
                  ))}
                  <tr style={{ background: '#f7fafc' }}>
                    <td colSpan={3}>
                      <strong>Subtotal</strong>
                    </td>
                    <td>
                      <strong>
                        $
                        {deptFees
                          .reduce((s, f) => s + (f.amount || 0), 0)
                          .toLocaleString()}
                      </strong>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}

/* ─── Student Fees ─── */
function StudentFees() {
  const { user } = useAuth();
  const [currentYear, setCurrentYear] = useState(null);
  const [fees, setFees] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const load = async () => {
      try {
        // Get student's current study year (includes studyYearId, departmentName, level, startYear, endYear)
        const yearRes = await userStudyYearService.getMyCurrent();
        const yearData = yearRes?.data || yearRes;
        setCurrentYear(yearData);

        if (yearData?.studyYearId) {
          // Fetch fees scoped to student's department + study year
          // The backend endpoint requires departmentId, but we may only have departmentName
          // Try the department+studyYear endpoint if we have an ID, otherwise fall back to studyYear-only
          // UserStudyYearDto doesn't expose departmentId directly but the Fee endpoint needs it
          // Use the study-year-only endpoint and filter on the frontend by student's level
          const feeRes = await feeService.getByStudyYear(yearData.studyYearId);
          const allFees = feeRes?.data || feeRes || [];

          // Filter: only fees matching student's department name (if available) and/or level
          const studentDept = yearData.departmentName || user?.departmentName;
          const studentLevel =
            yearData.level || yearData.levelName || user?.level;

          const filtered = allFees.filter(f => {
            const deptMatch =
              !studentDept ||
              (f.departmentName || '').toLowerCase() ===
                studentDept.toLowerCase();
            const levelMatch = !studentLevel || f.level === studentLevel;
            return deptMatch && levelMatch;
          });

          setFees(filtered);
        }
      } catch (e) {
        toast.error('Failed to load your fee information');
      }
      setLoading(false);
    };
    load();
  }, []);

  if (loading)
    return (
      <div className="page-container">
        <div className="spinner" />
      </div>
    );

  return (
    <div className="page-container">
      <div className="page-header">
        <h1>
          <FiDollarSign style={{ marginRight: 8 }} />
          My Fees
        </h1>
        <p>View fee structure for your current study year</p>
      </div>

      {/* Info cards */}
      {currentYear && (
        <div className="stats-grid" style={{ marginBottom: 24 }}>
          <div className="card stat-card">
            <div className="stat-icon blue">
              <FiCalendar />
            </div>
            <div className="stat-info">
              <h3>
                {currentYear.startYear} — {currentYear.endYear}
              </h3>
              <p>Current Study Year</p>
            </div>
          </div>
          <div className="card stat-card">
            <div className="stat-icon orange">
              <FiGrid />
            </div>
            <div className="stat-info">
              <h3>
                {currentYear.departmentName || user?.departmentName || '—'}
              </h3>
              <p>Department</p>
            </div>
          </div>
          <div className="card stat-card">
            <div className="stat-icon green">
              <FiDollarSign />
            </div>
            <div className="stat-info">
              <h3>
                {LEVEL_LABELS[currentYear.level || currentYear.levelName] ||
                  currentYear.levelName ||
                  '—'}
              </h3>
              <p>Level</p>
            </div>
          </div>
        </div>
      )}

      {!currentYear && (
        <div className="card empty-state">
          <h3>No current study year</h3>
          <p>
            You are not assigned to any active study year. Please contact your
            administrator.
          </p>
        </div>
      )}

      {currentYear && fees.length === 0 && (
        <div className="card empty-state">
          <h3>No fees found</h3>
          <p>No fees are configured for your current study year and level.</p>
        </div>
      )}

      {fees.length > 0 && (
        <div className="card">
          <div
            style={{
              display: 'flex',
              justifyContent: 'space-between',
              alignItems: 'center',
              marginBottom: 16,
            }}
          >
            <h3>Fee Breakdown</h3>
            <span className="badge badge-info">
              {fees.length} fee{fees.length !== 1 ? 's' : ''}
            </span>
          </div>
          <table>
            <thead>
              <tr>
                <th>Type</th>
                <th>Level</th>
                <th>Description</th>
                <th>Amount</th>
              </tr>
            </thead>
            <tbody>
              {fees.map(f => (
                <tr key={f.id}>
                  <td>
                    <span
                      className={`badge ${f.type === 'Academic' ? 'badge-info' : 'badge-warning'}`}
                    >
                      {FEE_TYPE_LABELS[f.type] || f.type}
                    </span>
                  </td>
                  <td>{LEVEL_LABELS[f.level] || f.level}</td>
                  <td>{f.description || '—'}</td>
                  <td>
                    <strong>${f.amount?.toLocaleString()}</strong>
                  </td>
                </tr>
              ))}
              <tr style={{ background: '#f7fafc', fontWeight: 700 }}>
                <td colSpan={3}>
                  <strong>Total</strong>
                </td>
                <td>
                  <strong
                    style={{ fontSize: '1.1rem', color: 'var(--primary)' }}
                  >
                    $
                    {fees
                      .reduce((s, f) => s + (f.amount || 0), 0)
                      .toLocaleString()}
                  </strong>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
