import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { FiBook, FiChevronRight, FiArrowLeft } from 'react-icons/fi';
import { semesterService } from '../../services/otherServices';
import { toast } from 'react-toastify';

const SEMESTER_LABELS = {
  First_Semester: 'First Semester',
  Second_Semester: 'Second Semester',
  Summer: 'Summer Semester',
};

export default function StudyYearSemesters() {
  const { studyYearId } = useParams();
  const navigate = useNavigate();
  const [semesters, setSemesters] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const load = async () => {
      try {
        const res = await semesterService.getByYear(studyYearId);
        setSemesters(res?.data || res || []);
      } catch (e) {
        toast.error('Failed to load semesters');
      }
      setLoading(false);
    };
    load();
  }, [studyYearId]);

  if (loading) return <div className="page-container"><div className="spinner" /></div>;

  return (
    <div className="page-container">
      <div className="page-header">
        <button
          className="btn btn-ghost btn-sm"
          onClick={() => navigate('/student/my-study-years')}
          style={{ marginBottom: 12 }}
        >
          <FiArrowLeft /> Back to Study Years
        </button>
        <h1><FiBook style={{ marginRight: 8 }} />Semesters</h1>
        <p>Study Year ID: {studyYearId}</p>
      </div>

      {semesters.length === 0 ? (
        <div className="card empty-state">
          <h3>No semesters found</h3>
          <p>This study year has no semesters yet.</p>
        </div>
      ) : (
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(320px, 1fr))', gap: 20 }}>
          {semesters.map(sem => (
            <div
              className="card"
              key={sem.id}
              style={{ cursor: 'pointer' }}
              onClick={() => navigate(`/student/study-year/${studyYearId}/semester/${sem.id}/courses`)}
            >
              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <div>
                  <h3 style={{ fontSize: '1.05rem', marginBottom: 6 }}>
                    {SEMESTER_LABELS[sem.title] || sem.title}
                  </h3>
                  <p style={{ color: 'var(--text-light)', fontSize: '0.85rem' }}>
                    {sem.startDate && new Date(sem.startDate).toLocaleDateString()} — {sem.endDate && new Date(sem.endDate).toLocaleDateString()}
                  </p>
                </div>
                <FiChevronRight size={20} color="var(--text-light)" />
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
