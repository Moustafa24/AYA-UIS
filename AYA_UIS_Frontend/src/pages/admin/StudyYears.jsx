import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import {
  FiPlus,
  FiCalendar,
  FiDollarSign,
  FiChevronRight,
} from 'react-icons/fi';
import {
  studyYearService,
  semesterService,
} from '../../services/otherServices';
import { toast } from 'react-toastify';

export default function StudyYears() {
  const [studyYears, setStudyYears] = useState([]);
  const [semesters, setSemesters] = useState({});
  const [loading, setLoading] = useState(true);
  const [modal, setModal] = useState(null);
  const [expandedYear, setExpandedYear] = useState(null);
  const navigate = useNavigate();
  const [yearForm, setYearForm] = useState({
    startYear: new Date().getFullYear(),
    endYear: new Date().getFullYear() + 1,
  });
  const [semForm, setSemForm] = useState({
    title: 'First_Semester',
    startDate: '',
    endDate: '',
    studyYearId: null,
  });

  const load = async () => {
    setLoading(true);
    try {
      const res = await studyYearService.getAll();
      setStudyYears(res?.data || res || []);
    } catch (e) {
      toast.error('Failed to load study years');
    }
    setLoading(false);
  };

  useEffect(() => {
    load();
  }, []);

  const loadSemesters = async yearId => {
    try {
      const res = await semesterService.getByYear(yearId);
      setSemesters(prev => ({ ...prev, [yearId]: res?.data || res || [] }));
    } catch (e) {
      toast.error('Failed to load semesters');
    }
  };

  const toggleExpand = yearId => {
    if (expandedYear === yearId) {
      setExpandedYear(null);
    } else {
      setExpandedYear(yearId);
      if (!semesters[yearId]) loadSemesters(yearId);
    }
  };

  const createYear = async e => {
    e.preventDefault();
    try {
      await studyYearService.create({
        startYear: parseInt(yearForm.startYear),
        endYear: parseInt(yearForm.endYear),
      });
      toast.success('Study year created');
      setModal(null);
      load();
    } catch (err) {
      toast.error(err?.errorMessage || 'Failed');
    }
  };

  const createSemester = async e => {
    e.preventDefault();
    try {
      await semesterService.create(semForm.studyYearId, {
        title: semForm.title,
        startDate: semForm.startDate,
        endDate: semForm.endDate,
      });
      toast.success('Semester created');
      setModal(null);
      loadSemesters(semForm.studyYearId);
    } catch (err) {
      toast.error(err?.errorMessage || 'Failed');
    }
  };

  if (loading)
    return (
      <div className="page-container">
        <div className="spinner" />
      </div>
    );

  return (
    <div className="page-container">
      <div
        className="page-header"
        style={{
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'center',
        }}
      >
        <div>
          <h1>
            <FiCalendar style={{ marginRight: 8 }} />
            Study Years & Semesters
          </h1>
          <p>Manage academic years, semesters, and view fees</p>
        </div>
        <div style={{ display: 'flex', gap: 12 }}>
          <button className="btn btn-primary" onClick={() => setModal('year')}>
            <FiPlus /> New Study Year
          </button>
        </div>
      </div>

      {studyYears.length === 0 ? (
        <div className="card empty-state">
          <h3>No study years found</h3>
          <p>Create a new study year to get started.</p>
        </div>
      ) : (
        <div style={{ display: 'grid', gap: 16 }}>
          {studyYears.map(sy => (
            <div
              className="card"
              key={sy.id}
              style={{ padding: 0, overflow: 'hidden' }}
            >
              {/* Study year header */}
              <div
                style={{
                  display: 'flex',
                  justifyContent: 'space-between',
                  alignItems: 'center',
                  padding: '20px 24px',
                  cursor: 'pointer',
                }}
                onClick={() => toggleExpand(sy.id)}
              >
                <div style={{ display: 'flex', alignItems: 'center', gap: 16 }}>
                  <div
                    style={{
                      width: 48,
                      height: 48,
                      borderRadius: 'var(--radius-md)',
                      background: '#ebf4ff',
                      color: 'var(--info)',
                      display: 'flex',
                      alignItems: 'center',
                      justifyContent: 'center',
                      fontSize: '1.2rem',
                    }}
                  >
                    <FiCalendar />
                  </div>
                  <div>
                    <h3 style={{ fontSize: '1.1rem', marginBottom: 2 }}>
                      {sy.startYear} — {sy.endYear}
                    </h3>
                    <span
                      style={{
                        color: 'var(--text-light)',
                        fontSize: '0.85rem',
                      }}
                    >
                      ID: {sy.id}
                    </span>
                  </div>
                </div>
                <div style={{ display: 'flex', alignItems: 'center', gap: 12 }}>
                  {sy.isCurrent && (
                    <span className="badge badge-success">Current</span>
                  )}
                  <button
                    className="btn btn-sm btn-accent"
                    onClick={e => {
                      e.stopPropagation();
                      navigate(`/admin/study-years/${sy.id}/fees`);
                    }}
                  >
                    <FiDollarSign /> Fees
                  </button>
                  <button
                    className="btn btn-sm btn-ghost"
                    onClick={e => {
                      e.stopPropagation();
                      setSemForm({ ...semForm, studyYearId: sy.id });
                      setModal('semester');
                    }}
                  >
                    <FiPlus /> Semester
                  </button>
                  <FiChevronRight
                    style={{
                      transition: 'transform 0.2s',
                      transform:
                        expandedYear === sy.id ? 'rotate(90deg)' : 'rotate(0)',
                      color: 'var(--text-light)',
                    }}
                  />
                </div>
              </div>

              {/* Expanded semesters */}
              {expandedYear === sy.id && (
                <div
                  style={{
                    borderTop: '1px solid var(--border)',
                    padding: '16px 24px',
                    background: '#f7fafc',
                  }}
                >
                  <h4
                    style={{
                      fontSize: '0.9rem',
                      marginBottom: 12,
                      color: 'var(--text-light)',
                    }}
                  >
                    Semesters
                  </h4>
                  {!semesters[sy.id] ? (
                    <div className="spinner" style={{ margin: '12px auto' }} />
                  ) : semesters[sy.id].length === 0 ? (
                    <p
                      style={{
                        color: 'var(--text-light)',
                        fontSize: '0.85rem',
                      }}
                    >
                      No semesters created yet
                    </p>
                  ) : (
                    <table>
                      <thead>
                        <tr>
                          <th>Title</th>
                          <th>Start Date</th>
                          <th>End Date</th>
                        </tr>
                      </thead>
                      <tbody>
                        {semesters[sy.id].map(s => (
                          <tr key={s.id}>
                            <td>{s.title?.replace('_', ' ')}</td>
                            <td>
                              {new Date(s.startDate).toLocaleDateString()}
                            </td>
                            <td>{new Date(s.endDate).toLocaleDateString()}</td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  )}
                </div>
              )}
            </div>
          ))}
        </div>
      )}

      {/* Create Study Year Modal */}
      {modal === 'year' && (
        <div className="modal-overlay" onClick={() => setModal(null)}>
          <div className="modal" onClick={e => e.stopPropagation()}>
            <h2>Create Study Year</h2>
            <form onSubmit={createYear}>
              <div className="form-row">
                <div className="form-group">
                  <label>Start Year</label>
                  <input
                    type="number"
                    className="form-control"
                    value={yearForm.startYear}
                    onChange={e =>
                      setYearForm({ ...yearForm, startYear: e.target.value })
                    }
                    required
                  />
                </div>
                <div className="form-group">
                  <label>End Year</label>
                  <input
                    type="number"
                    className="form-control"
                    value={yearForm.endYear}
                    onChange={e =>
                      setYearForm({ ...yearForm, endYear: e.target.value })
                    }
                    required
                  />
                </div>
              </div>
              <div className="form-actions">
                <button type="submit" className="btn btn-primary">
                  Create
                </button>
                <button
                  type="button"
                  className="btn btn-ghost"
                  onClick={() => setModal(null)}
                >
                  Cancel
                </button>
              </div>
            </form>
          </div>
        </div>
      )}

      {/* Create Semester Modal */}
      {modal === 'semester' && (
        <div className="modal-overlay" onClick={() => setModal(null)}>
          <div className="modal" onClick={e => e.stopPropagation()}>
            <h2>Create Semester</h2>
            <form onSubmit={createSemester}>
              <div className="form-group">
                <label>Study Year</label>
                <select
                  className="form-control"
                  value={semForm.studyYearId || ''}
                  onChange={e =>
                    setSemForm({
                      ...semForm,
                      studyYearId: parseInt(e.target.value),
                    })
                  }
                  required
                >
                  <option value="">Select study year</option>
                  {studyYears.map(sy => (
                    <option key={sy.id} value={sy.id}>
                      {sy.startYear} — {sy.endYear}
                    </option>
                  ))}
                </select>
              </div>
              <div className="form-group">
                <label>Title</label>
                <select
                  className="form-control"
                  value={semForm.title}
                  onChange={e =>
                    setSemForm({ ...semForm, title: e.target.value })
                  }
                >
                  <option value="First_Semester">First Semester</option>
                  <option value="Second_Semester">Second Semester</option>
                  <option value="Summer">Summer</option>
                </select>
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Start Date</label>
                  <input
                    type="date"
                    className="form-control"
                    value={semForm.startDate}
                    onChange={e =>
                      setSemForm({ ...semForm, startDate: e.target.value })
                    }
                    required
                  />
                </div>
                <div className="form-group">
                  <label>End Date</label>
                  <input
                    type="date"
                    className="form-control"
                    value={semForm.endDate}
                    onChange={e =>
                      setSemForm({ ...semForm, endDate: e.target.value })
                    }
                    required
                  />
                </div>
              </div>
              <div className="form-actions">
                <button type="submit" className="btn btn-primary">
                  Create
                </button>
                <button
                  type="button"
                  className="btn btn-ghost"
                  onClick={() => setModal(null)}
                >
                  Cancel
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
