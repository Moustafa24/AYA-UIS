import React, { useEffect, useState } from 'react';
import { FiClipboard, FiCheck, FiX, FiClock } from 'react-icons/fi';
import registrationService from '../../services/registrationService';
import {
  REGISTRATION_STATUS,
  GRADE_LABELS,
  COURSE_PROGRESS,
} from '../../constants';
import { toast } from 'react-toastify';

export default function AdminRegistrations() {
  // Admin can search registrations by year/semester
  const [yearId, setYearId] = useState('');
  const [semId, setSemId] = useState('');
  const [registrations, setRegistrations] = useState([]);
  const [loading, setLoading] = useState(false);
  const [editModal, setEditModal] = useState(null);
  const [editForm, setEditForm] = useState({ status: 'Approved', reason: '' });

  const search = async () => {
    if (!yearId) return;
    setLoading(true);
    try {
      let res;
      if (semId)
        res = await registrationService.getBySemester(
          parseInt(yearId),
          parseInt(semId)
        );
      else res = await registrationService.getByYear(parseInt(yearId));
      setRegistrations(res?.data || res || []);
    } catch (e) {
      toast.error('Failed to load');
    }
    setLoading(false);
  };

  const handleUpdate = async e => {
    e.preventDefault();
    try {
      await registrationService.update(editModal.id, editForm);
      toast.success('Registration updated');
      setEditModal(null);
      search();
    } catch (err) {
      toast.error(err?.errorMessage || 'Failed');
    }
  };

  const handleDelete = async id => {
    if (!window.confirm('Delete this registration?')) return;
    try {
      await registrationService.del(id);
      toast.success('Registration deleted');
      search();
    } catch (e) {
      toast.error('Failed');
    }
  };

  const statusBadge = s => {
    const map = {
      Approved: 'badge-success',
      Pending: 'badge-warning',
      Rejected: 'badge-danger',
      Suspended: 'badge-neutral',
    };
    return <span className={`badge ${map[s] || 'badge-neutral'}`}>{s}</span>;
  };

  return (
    <div className="page-container">
      <div className="page-header">
        <h1>
          <FiClipboard style={{ marginRight: 8 }} />
          Registrations
        </h1>
        <p>View and manage course registrations</p>
      </div>

      <div className="card" style={{ marginBottom: 20 }}>
        <div
          style={{
            display: 'flex',
            gap: 12,
            alignItems: 'end',
            flexWrap: 'wrap',
          }}
        >
          <div className="form-group" style={{ marginBottom: 0 }}>
            <label>Study Year ID</label>
            <input
              type="number"
              className="form-control"
              value={yearId}
              onChange={e => setYearId(e.target.value)}
              style={{ width: 140 }}
            />
          </div>
          <div className="form-group" style={{ marginBottom: 0 }}>
            <label>Semester ID (optional)</label>
            <input
              type="number"
              className="form-control"
              value={semId}
              onChange={e => setSemId(e.target.value)}
              style={{ width: 160 }}
            />
          </div>
          <button className="btn btn-primary" onClick={search}>
            Search
          </button>
        </div>
      </div>

      {loading ? (
        <div className="spinner" />
      ) : (
        registrations.length > 0 && (
          <div className="card">
            <div className="table-container">
              <table>
                <thead>
                  <tr>
                    <th>ID</th>
                    <th>Course</th>
                    <th>Status</th>
                    <th>Progress</th>
                    <th>Grade</th>
                    <th>Passed</th>
                    <th>Actions</th>
                  </tr>
                </thead>
                <tbody>
                  {registrations.map(r => (
                    <tr key={r.id}>
                      <td>{r.id}</td>
                      <td>
                        <strong>{r.course?.code}</strong> — {r.course?.name}
                      </td>
                      <td>{statusBadge(r.status)}</td>
                      <td>
                        <span className="badge badge-info">{r.progress}</span>
                      </td>
                      <td>{GRADE_LABELS[r.grade] || r.grade || '—'}</td>
                      <td>
                        {r.isPassed ? (
                          <FiCheck color="var(--success)" />
                        ) : (
                          <FiX color="var(--text-light)" />
                        )}
                      </td>
                      <td style={{ display: 'flex', gap: 6 }}>
                        <button
                          className="btn btn-ghost btn-sm"
                          onClick={() => {
                            setEditForm({
                              status: r.status,
                              reason: r.reason || '',
                            });
                            setEditModal(r);
                          }}
                        >
                          Edit
                        </button>
                        <button
                          className="btn btn-danger btn-sm"
                          onClick={() => handleDelete(r.id)}
                        >
                          <FiX />
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        )
      )}

      {editModal && (
        <div className="modal-overlay" onClick={() => setEditModal(null)}>
          <div className="modal" onClick={e => e.stopPropagation()}>
            <h2>Update Registration #{editModal.id}</h2>
            <form onSubmit={handleUpdate}>
              <div className="form-group">
                <label>Status</label>
                <select
                  className="form-control"
                  value={editForm.status}
                  onChange={e =>
                    setEditForm({ ...editForm, status: e.target.value })
                  }
                >
                  {Object.values(REGISTRATION_STATUS).map(s => (
                    <option key={s} value={s}>
                      {s}
                    </option>
                  ))}
                </select>
              </div>
              <div className="form-group">
                <label>Reason</label>
                <textarea
                  className="form-control"
                  rows={2}
                  value={editForm.reason}
                  onChange={e =>
                    setEditForm({ ...editForm, reason: e.target.value })
                  }
                />
              </div>
              <div className="form-actions">
                <button type="submit" className="btn btn-primary">
                  Update
                </button>
                <button
                  type="button"
                  className="btn btn-ghost"
                  onClick={() => setEditModal(null)}
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
