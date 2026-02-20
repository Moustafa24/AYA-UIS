import React, { useEffect, useState } from 'react';
import { FiPlus, FiUsers, FiSearch } from 'react-icons/fi';
import { userService } from '../../services/otherServices';
import authService from '../../services/authService';
import departmentService from '../../services/departmentService';
import { LEVEL_LABELS } from '../../constants';
import { toast } from 'react-toastify';
// Note: Level is auto-determined by backend based on department's HasPreparatoryYear flag

export default function Students() {
  const [departments, setDepartments] = useState([]);
  const [search, setSearch] = useState('');
  const [student, setStudent] = useState(null);
  const [modal, setModal] = useState(null);
  const [form, setForm] = useState({
    email: '',
    password: '',
    userName: '',
    phoneNumber: '',
    displayName: '',
    academic_Code: '',
    gender: 'Male',
    departmentId: '',
  });

  useEffect(() => {
    departmentService
      .getAll()
      .then(r => setDepartments(r?.data || r || []))
      .catch(() => {});
  }, []);

  const searchStudent = async () => {
    if (!search) return;
    try {
      const res = await userService.getByAcademicCode(search);
      setStudent(res?.data || res);
    } catch (e) {
      toast.error('Student not found');
      setStudent(null);
    }
  };

  const register = async e => {
    e.preventDefault();
    try {
      const deptId = parseInt(form.departmentId);
      const { departmentId, ...body } = form;
      await authService.registerStudent(deptId, body);
      toast.success('Student registered');
      setModal(null);
    } catch (err) {
      toast.error(err?.errorMessage || err?.errors?.join(', ') || 'Failed');
    }
  };

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
            <FiUsers style={{ marginRight: 8 }} />
            Students
          </h1>
          <p>Search and register students</p>
        </div>
        <button
          className="btn btn-primary"
          onClick={() => setModal('register')}
        >
          <FiPlus /> Register Student
        </button>
      </div>

      <div className="card" style={{ marginBottom: 20 }}>
        <h3 style={{ marginBottom: 12 }}>Search Student by Academic Code</h3>
        <div style={{ display: 'flex', gap: 12 }}>
          <input
            className="form-control"
            style={{ maxWidth: 300 }}
            placeholder="Enter academic code..."
            value={search}
            onChange={e => setSearch(e.target.value)}
            onKeyDown={e => e.key === 'Enter' && searchStudent()}
          />
          <button className="btn btn-primary" onClick={searchStudent}>
            <FiSearch /> Search
          </button>
        </div>
      </div>

      {student && (
        <div className="card">
          <h3 style={{ marginBottom: 16 }}>Student Information</h3>
          <div
            style={{
              display: 'grid',
              gridTemplateColumns: 'repeat(auto-fit, minmax(200px,1fr))',
              gap: 16,
            }}
          >
            <div>
              <small style={{ color: 'var(--text-light)' }}>Name</small>
              <p>
                <strong>{student.displayName}</strong>
              </p>
            </div>
            <div>
              <small style={{ color: 'var(--text-light)' }}>Email</small>
              <p>{student.email}</p>
            </div>
            <div>
              <small style={{ color: 'var(--text-light)' }}>
                Academic Code
              </small>
              <p>{student.academicCode}</p>
            </div>
            <div>
              <small style={{ color: 'var(--text-light)' }}>Level</small>
              <p>
                <span className="badge badge-info">
                  {LEVEL_LABELS[student.level] || student.level}
                </span>
              </p>
            </div>
            <div>
              <small style={{ color: 'var(--text-light)' }}>Department</small>
              <p>{student.departmentName || '—'}</p>
            </div>
            <div>
              <small style={{ color: 'var(--text-light)' }}>GPA</small>
              <p>{student.totalGPA?.toFixed(2) || '—'}</p>
            </div>
            <div>
              <small style={{ color: 'var(--text-light)' }}>Credits</small>
              <p>
                {student.totalCredits || 0} / {student.allowedCredits || '—'}
              </p>
            </div>
            <div>
              <small style={{ color: 'var(--text-light)' }}>
                Specialization
              </small>
              <p>{student.specialization || '—'}</p>
            </div>
          </div>
        </div>
      )}

      {modal === 'register' && (
        <div className="modal-overlay" onClick={() => setModal(null)}>
          <div
            className="modal"
            style={{ maxWidth: 600 }}
            onClick={e => e.stopPropagation()}
          >
            <h2>Register New Student</h2>
            <form onSubmit={register}>
              <div className="form-row">
                <div className="form-group">
                  <label>Display Name</label>
                  <input
                    className="form-control"
                    value={form.displayName}
                    onChange={e =>
                      setForm({ ...form, displayName: e.target.value })
                    }
                    required
                  />
                </div>
                <div className="form-group">
                  <label>Username</label>
                  <input
                    className="form-control"
                    value={form.userName}
                    onChange={e =>
                      setForm({ ...form, userName: e.target.value })
                    }
                    required
                  />
                </div>
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Email</label>
                  <input
                    type="email"
                    className="form-control"
                    value={form.email}
                    onChange={e => setForm({ ...form, email: e.target.value })}
                    required
                  />
                </div>
                <div className="form-group">
                  <label>Password</label>
                  <input
                    type="password"
                    className="form-control"
                    value={form.password}
                    onChange={e =>
                      setForm({ ...form, password: e.target.value })
                    }
                    required
                  />
                </div>
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Academic Code</label>
                  <input
                    className="form-control"
                    value={form.academic_Code}
                    onChange={e =>
                      setForm({ ...form, academic_Code: e.target.value })
                    }
                    required
                  />
                </div>
                <div className="form-group">
                  <label>Phone</label>
                  <input
                    className="form-control"
                    value={form.phoneNumber}
                    onChange={e =>
                      setForm({ ...form, phoneNumber: e.target.value })
                    }
                    required
                  />
                </div>
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Department</label>
                  <select
                    className="form-control"
                    value={form.departmentId}
                    onChange={e =>
                      setForm({ ...form, departmentId: e.target.value })
                    }
                    required
                  >
                    <option value="">Select...</option>
                    {departments.map(d => (
                      <option key={d.id} value={d.id}>
                        {d.name}
                      </option>
                    ))}
                  </select>
                </div>
              </div>
              <div className="form-group">
                <label>Gender</label>
                <select
                  className="form-control"
                  value={form.gender}
                  onChange={e => setForm({ ...form, gender: e.target.value })}
                >
                  <option value="Male">Male</option>
                  <option value="Female">Female</option>
                </select>
              </div>
              <div className="form-actions">
                <button type="submit" className="btn btn-primary">
                  Register
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
