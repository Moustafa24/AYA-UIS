import React, { useState } from 'react';
import { FiArrowUp, FiUsers, FiSearch } from 'react-icons/fi';
import { userStudyYearService } from '../../services/otherServices';
import { toast } from 'react-toastify';

export default function PromoteStudents() {
  const [academicCode, setAcademicCode] = useState('');
  const [promoteAllLoading, setPromoteAllLoading] = useState(false);
  const [promoteOneLoading, setPromoteOneLoading] = useState(false);

  const handlePromoteAll = async () => {
    if (
      !window.confirm(
        'Are you sure you want to promote ALL eligible students to the next study year? This action cannot be undone.'
      )
    )
      return;
    setPromoteAllLoading(true);
    try {
      const res = await userStudyYearService.promoteAll();
      toast.success(
        res?.message ||
          res?.data?.message ||
          'All eligible students promoted successfully!'
      );
    } catch (err) {
      toast.error(
        err?.errorMessage || err?.message || 'Failed to promote students'
      );
    }
    setPromoteAllLoading(false);
  };

  const handlePromoteStudent = async (e) => {
    e.preventDefault();
    if (!academicCode.trim()) {
      toast.warning('Please enter an academic code');
      return;
    }
    setPromoteOneLoading(true);
    try {
      const res = await userStudyYearService.promoteStudent(academicCode.trim());
      toast.success(res?.message || res?.data?.message || `Student ${academicCode} promoted successfully!`);
      setAcademicCode('');
    } catch (err) {
      toast.error(err?.errorMessage || err?.message || 'Failed to promote student');
    }
    setPromoteOneLoading(false);
  };

  return (
    <div className="page-container">
      <div className="page-header">
        <h1><FiArrowUp style={{ marginRight: 8 }} />Promote Students</h1>
        <p>Promote students to the current study year</p>
      </div>

      {/* Promote All */}
      <div className="card" style={{ marginBottom: 24 }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: 16, marginBottom: 16 }}>
          <div className="stat-icon blue"><FiUsers /></div>
          <div>
            <h3 style={{ fontSize: '1.1rem', marginBottom: 4 }}>Promote All Students</h3>
            <p style={{ color: 'var(--text-light)', fontSize: '0.85rem' }}>
              Promote all non-graduated students to the next year level. This will move students from preparatory to 1st year, 1st to 2nd, 2nd to 3rd, and 3rd to 4th year.
            </p>
          </div>
        </div>
        <button
          className="btn btn-primary"
          onClick={handlePromoteAll}
          disabled={promoteAllLoading}
          style={{ minWidth: 180 }}
        >
          {promoteAllLoading ? (
            <>
              <span className="spinner" style={{ width: 16, height: 16, margin: 0, borderWidth: 2 }} />
              Promoting...
            </>
          ) : (
            <><FiArrowUp /> Promote All Students</>
          )}
        </button>
      </div>

      {/* Promote Individual */}
      <div className="card">
        <div style={{ display: 'flex', alignItems: 'center', gap: 16, marginBottom: 16 }}>
          <div className="stat-icon orange"><FiSearch /></div>
          <div>
            <h3 style={{ fontSize: '1.1rem', marginBottom: 4 }}>Promote Individual Student</h3>
            <p style={{ color: 'var(--text-light)', fontSize: '0.85rem' }}>
              Promote a specific student by their academic code.
            </p>
          </div>
        </div>
        <form onSubmit={handlePromoteStudent} style={{ display: 'flex', gap: 12, alignItems: 'end' }}>
          <div className="form-group" style={{ flex: 1, maxWidth: 400, marginBottom: 0 }}>
            <label>Academic Code</label>
            <input
              className="form-control"
              placeholder="Enter student academic code..."
              value={academicCode}
              onChange={(e) => setAcademicCode(e.target.value)}
            />
          </div>
          <button
            type="submit"
            className="btn btn-accent"
            disabled={promoteOneLoading}
            style={{ minWidth: 160, height: 42 }}
          >
            {promoteOneLoading ? (
              <>
                <span className="spinner" style={{ width: 16, height: 16, margin: 0, borderWidth: 2 }} />
                Promoting...
              </>
            ) : (
              <><FiArrowUp /> Promote Student</>
            )}
          </button>
        </form>
      </div>
    </div>
  );
}
